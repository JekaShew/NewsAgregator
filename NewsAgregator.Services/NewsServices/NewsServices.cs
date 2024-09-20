using AutoMapper;
using HtmlAgilityPack;
using Mapper.Mappers.ParametersMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using Python.Runtime;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace NewsAgregator.Services.NewsServices
{
    public class NewsServices : INewsServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly ILogger<NewsServices> _logger;
        private readonly IAccountServices _accountServices;

        public NewsServices(AppDBContext appDBContext, ILogger<NewsServices> logger, IAccountServices accountServices)
        {
            _appDBContext = appDBContext;
            _logger = logger;
            _accountServices = accountServices;
        }

        public async Task<NewsParameters> GetNewsParametersAsync()
        {
            var newsParameters = new NewsParameters()
            {
                NewsStatuses = (await _appDBContext.NewsStatuses.ToListAsync()).Select(ns=> NewsParametersMapper.NewsStatusToParameter(ns)).ToList(),               
            };
            return newsParameters;

        }

        public async Task AddNewsAsync(NewsVM newsVM)
        {
            var newNews = NewsMapper.NewsVMToNews(newsVM);
            newNews.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newNews);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteNewsAsync(Guid id)
        {
            _appDBContext.Newses.Remove(await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<NewsVM> TakeNewsByIdAsync(Guid id)
        {
            var news = NewsMapper.NewsToNewsVM(await _appDBContext.Newses
                .AsNoTracking()
                .Include(ns => ns.NewsStatus)
                .FirstOrDefaultAsync(n => n.Id == id));

            var newsParameters = await GetNewsParametersAsync();
            news.NewsStatuses = newsParameters.NewsStatuses;

            return news;
        }

        public async Task<List<NewsVM>> TakeNewsesAsync()
        {
            var newsVMs = (await _appDBContext.Newses
                .AsNoTracking()
                .Include(ns => ns.NewsStatus)
                .ToListAsync()).Select(n=> NewsMapper.NewsToNewsVM(n)).ToList();

            return newsVMs;
        }

        public async Task UpdateNewsAsync(NewsVM updatedNewsVM)
        {
            var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == updatedNewsVM.Id);

            if (news != null)
            {
                news.Title = updatedNewsVM.Title;
                news.Description = updatedNewsVM.Description;
                news.Text = updatedNewsVM.Text;
                news.TextHTML = updatedNewsVM.TextHTML;
                news.SourceUrl = updatedNewsVM.SourceUrl;
                news.Date = updatedNewsVM.Date;
                news.PositiveRating = updatedNewsVM.PositiveRating;
                news.NewsStatusId = updatedNewsVM.NewsStatusId;

                await _appDBContext.SaveChangesAsync();
            }
        }



        public async Task AggregateNewsAsync()
        {
            var sources = await _appDBContext.Sources.Where(x => !string.IsNullOrEmpty(x.RssUrl)).ToListAsync();
            var existedNewsUrls = await _appDBContext.Newses.Select(n => n.SourceUrl).ToListAsync();

            //var source = await _appDBContext.Sources.FirstOrDefaultAsync(s => s.BaseUrl.Contains("belta.by"));

            //newsVMRSSData.AddRange(await GetRssData(source, existedNewsUrls));

            var tasksForRSSData = new List<Task>();

            foreach (var source in sources)
            {

                try
                {
                    tasksForRSSData.Add(GetRssDataAsync(source, existedNewsUrls));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing RSS feed from {source.RssUrl}: {ex.Message?.ToString()}; {ex.InnerException?.Message}");
                    Console.WriteLine(ex.Message);
                }
            }
        

            var validRSSDataTasks = tasksForRSSData
                                        .Where(task => task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted).ToList();

            if (validRSSDataTasks.Count != 0 && validRSSDataTasks != null)
               await Task.WhenAll(validRSSDataTasks);
           
            var newsVMWithOutText = await GetNewsWithoutTextAsync();

            var newsesToScrap =  newsVMWithOutText.Where(n => n.SourceUrl.Contains("belkagomel")).ToList();

            var tasksScrappingData = new List<Task>();
            foreach (var newsTestScrap in newsesToScrap)
            {
                //var result = await UpdateNewsText(newsTestScrap);
                try
                {
                    tasksScrappingData.Add(UpdateNewsTextByScrappedData(newsTestScrap));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing Scrapping data from {newsTestScrap.SourceUrl}: {ex.Message} Inner Exceptiomn: {ex.InnerException?.Message}");
                }
            }

            var validTasksScrappingData = tasksScrappingData.Where(task => task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted).ToList();


            if (validTasksScrappingData.Count != 0 && validTasksScrappingData != null)
                await Task.WhenAll(validTasksScrappingData);


            //var resultText1 = await UpdateNewsText(newsVMWithOutText.FirstOrDefault(n=> n.SourceUrl.Contains("belta.by")));

            //var resultText2 = await UpdateNewsText(newsVMWithOutText.FirstOrDefault(n => n.SourceUrl.Contains("gp.by")));

            //var resultText3 = await UpdateNewsText(newsVMWithOutText.FirstOrDefault(n => n.SourceUrl.Contains("belkagomel.by")));

            //var resultText4 = await UpdateNewsText(newsVMWithOutText.FirstOrDefault(n => n.SourceUrl.Contains("sputnik.by")));
          
        }

        private async Task<List<NewsVM>> GetNewsWithoutTextAsync()
        {
            var newsVMWithOutText = (await _appDBContext.Newses.Where(n => string.IsNullOrEmpty(n.Text)).ToListAsync()).Select(n => NewsMapper.NewsToNewsVM(n)).ToList();

            return newsVMWithOutText;
        }

        private async Task<List<NewsVM>> GetNewsWithoutRateAsync()
        {
            var newsVMWithOutRate = (await _appDBContext.Newses.Where(n => n.PositiveRating.HasValue == false).ToListAsync()).Select(n => NewsMapper.NewsToNewsVM(n)).ToList();

            return newsVMWithOutRate;
        }

        public async Task GetRssDataAsync()
        {
            var sources = await _appDBContext.Sources.ToListAsync();
            var existedNewsUrls = await _appDBContext.Newses.Select(n => n.SourceUrl).ToListAsync();
            var tasksForRSSData = new List<Task>();

            foreach (var source in sources)
            {

                try
                {
                    tasksForRSSData.Add(GetRssDataAsync(source, existedNewsUrls));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing RSS feed from {source.RssUrl}: {ex.Message?.ToString()}; {ex.InnerException?.Message}");
                    Console.WriteLine(ex.Message);
                }
            }


            var validRSSDataTasks = tasksForRSSData
                                        .Where(task => task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted).ToList();

            if (validRSSDataTasks.Count != 0 && validRSSDataTasks != null)
                await Task.WhenAll(validRSSDataTasks);
        }

        public async Task UpdateNewsTextByScrappedData()
        {
            var newsVMs = await GetNewsWithoutTextAsync();
            var tasksScrappingData = new List<Task>();
            foreach (var newsVM in newsVMs)
            {
                try
                {
                    tasksScrappingData.Add(UpdateNewsTextByScrappedData(newsVM));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing Scrapping data from {newsVM.SourceUrl}: {ex.Message} Inner Exceptiomn: {ex.InnerException?.Message}");
                }
            }

            var validTasksScrappingData = tasksScrappingData.Where(task => task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted).ToList();


            if (validTasksScrappingData.Count != 0 && validTasksScrappingData != null)
                await Task.WhenAll(validTasksScrappingData);

        }

        private async Task GetRssDataAsync(Source source, List<string> existedNewsUrls)
        {
            if (source?.RssUrl != null)
            {
                try
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.Async = true;
                    using (var xmlreader = XmlReader.Create(source.RssUrl, settings))
                    {
                        var syndicationFeed = SyndicationFeed.Load(xmlreader);
                        Console.WriteLine(source.BaseUrl);
                        var newsVMs = syndicationFeed.Items
                            .Where(x => !existedNewsUrls.Contains(x.Links.FirstOrDefault().Uri.OriginalString))
                            .Select(x => new NewsVM
                            {
                                Id = Guid.NewGuid(),
                                Title = x.Title.Text,
                                Description = x.Summary.Text,
                                Date = x.PublishDate.DateTime,
                                SourceUrl = x.Links.FirstOrDefault().Uri.OriginalString,
                                SourceId = source.Id,
                            }).ToList();

                        if (newsVMs.Count != 0 && newsVMs != null)
                        {
                            var newsesRSSData = newsVMs.Where(vm => vm != null).Select(vm => NewsMapper.NewsVMToNews(vm));

                            await _appDBContext.Newses.AddRangeAsync(newsesRSSData);
                            await _appDBContext.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing Gettin Rss data from {source.RssUrl}: {ex.Message} Inner Exceptiomn: {ex.InnerException?.Message}");
                }  
            }    
        }

        private async Task UpdateNewsTextByScrappedData(NewsVM newsVM)
        {
            var web = new HtmlWeb();
            var doc = web.Load(newsVM.SourceUrl);
            
            var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == newsVM.Id);

            void RemoveExcludeElementsBelta(HtmlNode htmlNode)
            {
                // возможно нужно после фоток вставлять теги <br> чтобы переходить на новую строку(или в js стилях делать flex column???)
                var allChilNodes = htmlNode.ChildNodes.ToList();
                foreach (var child in allChilNodes)
                {
                    if (child.Name == "a" ||
                        child.Name == "script" ||
                        child.Name == "div" && (child.Attributes["class"]?.Value?.Contains("yandex_share_print") ?? false) ||
                        child.Name == "div" && (child.Attributes["class"]?.Value?.Contains("video_add") ?? false) ||
                        child.Name == "div" && (child.Attributes["class"]?.Value?.Contains("advertising_block") ?? false) ||
                        child.Name == "div" && (child.Attributes["class"]?.Value?.Contains("news_tags_block") ?? false))
                    {
                        child.Remove();
                    }
                    else
                    {
                        RemoveExcludeElementsBelta(child); 
                    }
                }
            }

            void RemoveExcludeElementsSputnik(HtmlNode htmlNode)
            {
                var allChilNodes = htmlNode.ChildNodes.ToList();
                foreach (var child in allChilNodes)
                {
                    if (
                        child.Name == "strong" ||
                        child.Name == "div" && (child.Attributes["data-type"]?.Value?.Contains("banner") ?? false) ||
                        child.Name == "div" && (child.Attributes["class"]?.Value?.Contains("article__quote-info") ?? false)
                        )
                    {
                        child.Remove();
                    }
                    else
                    {
                        RemoveExcludeElementsSputnik(child);
                    }
                }
            }

            void RemoveExcludeElementsBelkagomel(HtmlNode htmlNode)
            {
                // возможно нужно после фоток вставлять теги <br> чтобы переходить на новую строку(или в js стилях делать flex column???)
                var allChilNodes = htmlNode.ChildNodes.ToList();
                foreach (var child in allChilNodes)
                {
                    if (child.Name == "a" ||
                        child.Name == "script" ||
                        child.Name == "strong" ||
                        child.Name == "div" && (child.Attributes["class"]?.Value?.Contains("video-container") ?? false)
                        )
                    {
                        child.Remove();
                    }
                    else
                    {
                        RemoveExcludeElementsBelkagomel(child);
                    }
                }
            }

            string RemoveATags(string newsNode)
            {
                // пересмотреть варианты работы со строкой, оптимальный ли вариант постоянной конвертации ToString()
                // мб поопробовать использование регулярных выражений для поиска подстрок в строке

                string strA1 = "<a";
                char chrA1 = '>';
                string strA2 = "<\\a>";
                StringBuilder stringBuilder = new StringBuilder(newsNode);
                while (stringBuilder.ToString().Contains(strA1) || stringBuilder.ToString().Contains(strA2))
                {
                    if (stringBuilder.ToString().Contains(strA1))
                    {
                        int indexA1 = stringBuilder.ToString().IndexOf(strA1);

                        if (indexA1 >= 0)
                        {
                            int endIndex = stringBuilder.ToString().IndexOf(chrA1, indexA1) + 1;
                            string extractedPart = stringBuilder.ToString().Substring(indexA1, endIndex - indexA1);

                            stringBuilder = stringBuilder.Replace(extractedPart, "");
                        }
                    }

                    if (stringBuilder.ToString().Contains(strA2))
                    {
                        stringBuilder = stringBuilder.Replace(strA2, "");
                    }
                }
                return stringBuilder.ToString();
            }

            // not ready need last fixes
            if (newsVM.SourceUrl.Contains("belta.by"))
            {
                string newsTextNode;
                string newsHtmlNode = doc.DocumentNode.SelectSingleNode("//div[@class='news_img_slide']").InnerHtml;
                
                var newsAllNode = doc.DocumentNode.SelectSingleNode("//div[@class='js-mediator-article']");
                RemoveExcludeElementsBelta(newsAllNode);
                newsHtmlNode += "\n";
                newsHtmlNode += newsAllNode.InnerHtml;
                newsTextNode = newsAllNode.InnerText;                
            
                news.TextHTML = RemoveATags(newsHtmlNode);
                news.Text = newsTextNode;
            }

            //not ready need last fixes
            if (newsVM.SourceUrl.Contains("sputnik.by"))
            {
                // непонятно почему не находит фотку, возможно следует искать по блокам выше( тпиа родительский div с определенным классом???)
                string newsTextNode;
                string newsHtmlNode = doc.DocumentNode.SelectSingleNode("//img[@media-type='ar16x9']").InnerHtml;
                newsHtmlNode += "\n";
                newsHtmlNode += doc.DocumentNode.SelectSingleNode("//div[@class='article__announce-text']").InnerHtml;
                newsHtmlNode += "\n";

                var newsAllNode = doc.DocumentNode.SelectSingleNode("//div[@class='article__body']");
               
                RemoveExcludeElementsSputnik(newsAllNode);
                newsHtmlNode += newsAllNode.InnerHtml;
                newsTextNode = newsAllNode.InnerText;

                news.TextHTML = RemoveATags(newsHtmlNode);
                news.Text = newsTextNode;
            }

            //not ready need last fixes
            if (newsVM.SourceUrl.Contains("belkagomel.by"))
            {
                var newsAllNodes = doc.DocumentNode.SelectSingleNode("//div[@class='entry']");
                
                RemoveExcludeElementsBelkagomel(newsAllNodes);

                string newsHTMLNode = newsAllNodes.InnerHtml;
                string newsTextNode = newsAllNodes.InnerText;
                
                news.TextHTML = RemoveATags(newsHTMLNode);
                news.Text = newsTextNode;
            }


            //not ready 
            if (newsVM.SourceUrl.Contains("gp.by"))
            {
                var newsNode = doc.DocumentNode.SelectSingleNode("//article[@itemprop='description']").InnerHtml;

                news.TextHTML = newsNode;

            }

            await _appDBContext.SaveChangesAsync();
        }
        
        public async Task UpdateNewsRateAsync()
        {
            var newsVMWithoutRate = await GetNewsWithoutRateAsync();

            foreach(var newsVM in newsVMWithoutRate)
            {
                var message = newsVM.Text + " оцени позитивность текста и ответь только числом от 0 до 10 насколько позитивный этот текст";
                var responseString = CallAIRaterV1(message);

                int rateAI = -1;
                int.TryParse(responseString, out rateAI);
                if(rateAI != -1)
                {  
                    var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == newsVM.Id);
                    news.PositiveRating = rateAI;
                    await _appDBContext.SaveChangesAsync();
                }
                else
                    

            }

            

        }

        public string CallAIRaterV1(string message)
        {
            string responseString;
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(@"путь_к_папке_с_вашим_скриптом"); // Укажите путь к папке, где находится python_script.py

                dynamic pythonScript = Py.Import("python_script");
                responseString = pythonScript.chat_with_model(message);
                Console.WriteLine(responseString);
            }

            return responseString;
        }

        public string CallAIRaterV2(string message)
        {
            string responseString;

            PythonEngine.Initialize();

            using (Py.GIL())
            {
                dynamic g4f = Py.Import("g4f.client");
                dynamic Client = g4f.Client();
                dynamic response = Client.chat.completions.create(
                    model: "gpt-3.5-turbo",
                    messages: new[] { new { role = "user", content = message } }
                );

                responseString = response.choices[0].message.content;
            }

            PythonEngine.Shutdown();

            return responseString;
        }

        public string CallAIRaterV3(string message)
        {
            string responseString;

            Runtime.PythonDLL = @"C:\Python312\python312.dll";
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                var pythonScript = Py.Import("g4fLocalAPI");
                var messagePY = new PyString("message");
                var responseAI = pythonScript.InvokeMethod("chat_with_model", new PyObject[] { messagePY });
                responseString = responseAI.ToString();
            }

            PythonEngine.Shutdown();

            return responseString;
        }

        public async Task DeleteNewsWithBadRateAsync()
        {
            var allNewses = await TakeNewsesAsync();
            var toDeleteNewses = allNewses.Where(n => n.PositiveRating <=5).ToList();
            foreach(var news in toDeleteNewses)
            {
                await DeleteNewsAsync(news.Id);
            }

            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteOldNewsesAsync()
        {
            var allNewses = await TakeNewsesAsync();
            TimeSpan twoWeeks = TimeSpan.FromDays(14);
            var oldNewses = allNewses.Where(n => n.Date.Value >= DateTime.Now.AddDays(-14)).ToList();
            foreach (var news in oldNewses)
            {
                await DeleteNewsAsync(news.Id);

            }

            await _appDBContext.SaveChangesAsync();
        }

        public async Task<List<NewsVM>> TakeSuitableNewesAsync()
        {
            var allNewses = await TakeNewsesAsync();
            Guid? accountId = _accountServices.GetCurrentAccountId();
            var accountVM = await _accountServices.TakeAccountByIdAsync(accountId.Value);

            var suitableNewses = allNewses
                                .Where(n => n.PositiveRating >=
                                        (accountVM.DesiredNewsRating != null ? accountVM.DesiredNewsRating : 5)).ToList();
            return suitableNewses;
        }

    }
}

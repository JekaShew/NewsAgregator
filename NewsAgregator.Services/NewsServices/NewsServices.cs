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
using Azure;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

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
                NewsStatuses = (await _appDBContext.NewsStatuses.ToListAsync()).Select(ns => NewsParametersMapper.NewsStatusToParameter(ns)).ToList(),
            };
            return newsParameters;

        }

        public async Task AddNewsAsync(NewsVM newsVM)
        {
            var newNews = NewsMapper.NewsVMToNews(newsVM);
            newNews.Id = Guid.NewGuid();
            if(newsVM.NewsStatusId == null)
            {
                newNews.NewsStatusId = await _appDBContext.NewsStatuses.AsNoTracking().Where(ns => ns.Title.Equals("Disabled")).Select(ns => ns.Id).FirstOrDefaultAsync();
            }

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
                .ToListAsync()).Select(n => NewsMapper.NewsToNewsVM(n)).ToList();

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
                if (updatedNewsVM.NewsStatusId == null)
                {
                    news.NewsStatusId = await _appDBContext.NewsStatuses.AsNoTracking().Where(ns => ns.Title.Equals("Disabled")).Select(ns => ns.Id).FirstOrDefaultAsync();
                }

                await _appDBContext.SaveChangesAsync();
            }
        }



        public async Task AggregateNewsAsync()
        {
            var sources = await _appDBContext.Sources.Where(x => !string.IsNullOrEmpty(x.RssUrl)).ToListAsync();
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

            var newsVMWithOutText = await GetNewsWithoutTextAsync();
            // testing specific sourceUrl scrapping
            //var newsesToScrap = newsVMWithOutText.Where(n => n.SourceUrl.Contains("belkagomel")).ToList();
            var newsesToScrap = newsVMWithOutText.ToList();

            var tasksScrappingData = new List<Task>();
            foreach (var newsToScrap in newsesToScrap)
            {
                try
                {
                    tasksScrappingData.Add(UpdateNewsTextByScrappedData(newsToScrap));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing Scrapping data from {newsToScrap.SourceUrl}: {ex.Message} Inner Exceptiomn: {ex.InnerException?.Message}");
                }
            }

            var validTasksScrappingData = tasksScrappingData.Where(task => task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted).ToList();

            if (validTasksScrappingData.Count != 0 && validTasksScrappingData != null)
                await Task.WhenAll(validTasksScrappingData);
        }

        private async Task<List<NewsVM>> GetNewsWithoutTextAsync()
        {
            var newsVMWithOutText = (await _appDBContext.Newses.Where(n => string.IsNullOrEmpty(n.Text)).ToListAsync()).Select(n => NewsMapper.NewsToNewsVM(n)).ToList();

            return newsVMWithOutText;
        }

        private async Task<List<NewsVM>> GetNewsWithoutRateAsync()
        {
            var newsVMWithOutRate = (await _appDBContext.Newses.Where(n => n.PositiveRating.HasValue == false && !string.IsNullOrEmpty(n.Text)).ToListAsync()).Select(n => NewsMapper.NewsToNewsVM(n)).ToList();

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
                            newsVMs.Select(nvm => nvm.Description = Regex.Replace(nvm.Description, @"<[^>]*>", ""));
                            var disabledNewsStatusId = await _appDBContext.NewsStatuses.AsNoTracking().Where(ns => ns.Title.Equals("Disabled")).Select(ns => ns.Id).FirstOrDefaultAsync();
                            newsVMs.Select(nvm => nvm.NewsStatusId = disabledNewsStatusId);
                            
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

            string ClearTextFromUnnecessarySymbols(string nodeText)
            {
                nodeText = Regex.Replace(nodeText, @"[\t\r\n]+", "");
                //nodeText = Regex.Replace(nodeText, @"\s+", " ");
                //nodeText = nodeText.Trim();
               
                return nodeText;
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
                ClearTextFromUnnecessarySymbols(newsTextNode);
                news.Text = newsTextNode;
            }

            //not ready need last fixes
            if (newsVM.SourceUrl.Contains("sputnik.by"))
            {
                // непонятно почему не находит фотку, возможно следует искать по блокам выше( тпиа родительский div с определенным классом???)
                string newsTextNode;
                string newsHtmlNode;
                //string newsHtmlNode = doc.DocumentNode.SelectSingleNode("//img[@media-type='ar16x9']").InnerHtml;
                //newsHtmlNode += "\n";
                newsHtmlNode = doc.DocumentNode.SelectSingleNode("//div[@class='article__announce-text']").InnerHtml;
                newsHtmlNode += "\n";

                var newsAllNode = doc.DocumentNode.SelectSingleNode("//div[@class='article__body']");

                RemoveExcludeElementsSputnik(newsAllNode);
                newsHtmlNode += newsAllNode.InnerHtml;
                newsTextNode = newsAllNode.InnerText;

                news.TextHTML = RemoveATags(newsHtmlNode);
                ClearTextFromUnnecessarySymbols(newsTextNode);
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
                ClearTextFromUnnecessarySymbols(newsTextNode);
                news.Text = newsTextNode;
            }

            //not ready need to update scrapping method
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

            foreach (var newsVM in newsVMWithoutRate)
            {
                string responseString;
                int rateAI = -1;
                string retryProvidersScript = "g4flocalapi.py";
                string bixinProviderScript = "g4fbixinprovider.py";
                string blackboxProviderScript = "g4fblackboxprovider.py";
                string bingProviderScript = "g4fbingprovider.py";

                var message = "Оцени позитивность текста и в Ответ выдай мне ТОЛЬКО Одно число от 0 до 10 насколько позитивный этот текст без объяснений: " + newsVM.Text;

                responseString = CallAIRaterV5(message,bixinProviderScript);
                Match matchBixin = Regex.Match(responseString, @"\d+");
                if (matchBixin.Success)
                {
                    Console.WriteLine(newsVM.Title);
                    Console.WriteLine(matchBixin.Value);
                    var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == newsVM.Id);
                    rateAI = int.Parse(matchBixin.Value);
                    news.PositiveRating = rateAI;
                    
                    await _appDBContext.SaveChangesAsync();
                }
                else
                {
                    responseString = CallAIRaterV5(message, blackboxProviderScript);
                    Match matchBlackBox = Regex.Match(responseString, @"\d+");
                    if (matchBlackBox.Success)
                    {
                        Console.WriteLine(newsVM.Title);
                        Console.WriteLine(matchBlackBox.Value);
                        var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == newsVM.Id);
                        rateAI = int.Parse(matchBlackBox.Value);
                        news.PositiveRating = rateAI;

                        await _appDBContext.SaveChangesAsync();
                    }
                    else
                    {
                        responseString = CallAIRaterV5(message, bingProviderScript);
                        Match matchBing = Regex.Match(responseString, @"\d+");
                        if (matchBing.Success)
                        {
                            Console.WriteLine(newsVM.Title);
                            Console.WriteLine(matchBing.Value);
                            var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == newsVM.Id);
                            rateAI = int.Parse(matchBing.Value);
                            news.PositiveRating = rateAI;
                            await _appDBContext.SaveChangesAsync();
                        }
                        else
                        {
                            responseString = CallAIRaterV5(message, retryProvidersScript);
                            Match matchRetryProviders = Regex.Match(responseString, @"\d+");
                            if (matchRetryProviders.Success)
                            {
                                Console.WriteLine(newsVM.Title);
                                Console.WriteLine(matchRetryProviders.Value);
                                var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == newsVM.Id);
                                rateAI = int.Parse(matchRetryProviders.Value);
                                news.PositiveRating = rateAI;

                                await _appDBContext.SaveChangesAsync();
                            }
                        }
                    }
                }
                // log errors if they exists or unsupported gpt messages
            }
        }

        public string CallAIRaterV1(string message)
        {
            string responseString;
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(@"G:\С#_new\ITAcademy\NewsAgregator2\NewsAgregator\NewsAgregator.Services\NewsServices"); 

                dynamic pythonScript = Py.Import("g4flocalapi");
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

            Runtime.PythonDLL = @"C:\Users\1\AppData\Local\Programs\Python\Python312\python312.dll";
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                var pythonScript = Py.Import("g4flocalapi");
                var messagePY = new PyString(message);
                var responseAI = pythonScript.InvokeMethod("chat_with_model", new PyObject[] { messagePY });
                responseString = responseAI.ToString();
            }

            PythonEngine.Shutdown();

            return responseString;
        }


        public async Task<string> CallAIRaterV4(string message)
        {
            string responseString;


            var url = "http://localhost:1337/v1/chat/completions";

            var body = new
            {
                model = "gpt-3.5-turbo",
                stream = true,
                messages = new[]
                {
                    new { role = "assistant", content = message }
                }
            };

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(50);
                var jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(jsonResponse);

                //    foreach (var choice in choices)
                //    {
                //        Console.WriteLine(choice.message.content);
                //    }
            }


            return responseString = "-2";
        }

        public string CallAIRaterV5(string message, string scriptName)
        {
            string responseString;

            ProcessStartInfo ProcessInfo = new ProcessStartInfo();

            ProcessInfo.FileName = "python";
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardOutput = true;

            ProcessInfo.Arguments = $"{scriptName} \"{message}\"";
            Process myProcess = new Process();

            myProcess.StartInfo = ProcessInfo;

            myProcess.Start();

            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadToEnd();
            myProcess.WaitForExit();
            myProcess.Close();
            Console.WriteLine(myString);

            return myString;
        }

        public async Task DeleteNewsWithBadRateAsync()
        {
            var allNewses = await TakeNewsesAsync();
            var toDeleteNewses = allNewses.Where(n => n.PositiveRating <=4).ToList();
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

        public async Task<List<NewsVM>> TakeTopNewsesAsync()
        {
            var allNewses = await TakeNewsesAsync();
            var topNewses = allNewses
                                .OrderByDescending(n=> n.PositiveRating).ThenBy(n => n.Date).Take(6)               
                                .ToList();
            return topNewses;
        }

        public async Task<List<NewsVM>> TakeSuitableNewsesAsync()
        {
            var allNewses = await TakeNewsesAsync();
            Guid? accountId = _accountServices.GetCurrentAccountId();
            var accountVM = await _accountServices.TakeAccountByIdAsync(accountId.Value);

            var suitableNewses = allNewses
                                .Where(n => n.PositiveRating >=
                                        (accountVM.DesiredNewsRating != null ? accountVM.DesiredNewsRating : 5)).OrderBy(n => n.Date).ToList();
            return suitableNewses;
        }

    }
}

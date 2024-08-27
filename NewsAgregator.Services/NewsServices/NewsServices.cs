using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
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
        private readonly IMapper _mapper;
        private readonly ILogger<NewsServices> _logger;
        public NewsServices(AppDBContext appDBContext, IMapper mapper, ILogger<NewsServices> logger)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<NewsParameters> GetNewsParameters()
        {
            var newsParameters = new NewsParameters()
            {
                NewsStatuses = await _appDBContext.NewsStatuses.Select(ns => new Parameter { Id = ns.Id, Text = ns.Title }).ToListAsync(),
               
            };
            return newsParameters;

        }

        public async Task ConvertNewsParameters(NewsVM newsVM)
        {
            var newsStatus = await _appDBContext.NewsStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == newsVM.NewsStatusId);

            newsVM.FromDataModel(newsStatus);
        }

        public async Task AddNews(NewsVM news)
        {
            var newNews = _mapper.Map<Data.Models.News>(news);
            newNews.Id = Guid.NewGuid();
            newNews.NewsStatus = null;

            await _appDBContext.AddAsync(newNews);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteNews(Guid id)
        {
            _appDBContext.Newses.Remove(await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<NewsVM> TakeNewsById(Guid id)
        {
            var news = _mapper.Map<NewsVM>(await _appDBContext.Newses
                .AsNoTracking()
                .Include(ns => ns.NewsStatus)
                .FirstOrDefaultAsync(n => n.Id == id));

            var newsParameters = await GetNewsParameters();
            await ConvertNewsParameters(news);
            news.NewsStatuses = newsParameters.NewsStatuses;

            return news;
        }

        public async Task<List<NewsVM>> TakeNewses()
        {
            var newsVMs = _mapper.Map<List<NewsVM>>(await _appDBContext.Newses
                .AsNoTracking()
                .Include(ns => ns.NewsStatus)
                .ToListAsync());

            foreach (var newsVM in newsVMs)
            {
                var newsStatus = await _appDBContext.NewsStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == newsVM.NewsStatusId);

                newsVM.FromDataModel(newsStatus);
            }

            return newsVMs;
        }

        public async Task UpdateNews(NewsVM updatedNews)
        {
            var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == updatedNews.Id);

            if (news != null)
            {
                news.Title = updatedNews.Title;
                news.Text = updatedNews.Text;
                news.Date = updatedNews.Date;
                news.PositiveRating = updatedNews.PositiveRating;
                news.NewsStatusId = updatedNews.NewsStatusId;
                news.NewsStatus = null;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }

        public async Task AggregateNews()
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
                    tasksForRSSData.Add(GetRssData(source, existedNewsUrls));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing RSS feed from {source.RssUrl}: {ex.Message?.ToString()}; {ex.InnerException?.Message}");
                    Console.WriteLine(ex.Message);
                }

            }
        //var testSource = await _appDBContext.Sources.FirstOrDefaultAsync(s => s.RssUrl.Contains("belkagomel"));
        //    try
        //    {
        //        var testResult = await GetRssData(testSource, existedNewsUrls);
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError($"Error processing RSS feed from {ex.InnerException}: {ex.Message}");
        //    }
            

            //if (newsVMsRssData.Count != 0 && newsVMsRssData != null)
            //{
            //    var newsesRSSData = _mapper.Map<List<News>>(newsVMsRssData.Where(vm => vm != null).Select(vm => vm));

            //    await _appDBContext.Newses.AddRangeAsync(newsesRSSData);
            //    await _appDBContext.SaveChangesAsync();
            //}
            //     var task = GetRssData(source, existedNewsUrls);
            //     if(task.Status == TaskStatus.Faulted)
            //      task.ContinueWith(t =>
            //           {
            //               if (t.Status == TaskStatus.Faulted)
            //               {
            //                   _logger.LogError($"Error processing RSS feed from {source.RssUrl}: {t.Exception?.InnerException?.Message} Inner Exceptiomn: {t.Exception?.InnerException?.InnerException?.Message}");
            //                   Console.WriteLine(t.Exception?.InnerException?.Message);

            //               }
            //           }, TaskContinuationOptions.OnlyOnFaulted);
            //     tasksForRSSData.Add(task);

            // }

            var validRSSDataTasks = tasksForRSSData.Where(task => task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted).ToList();


            if (validRSSDataTasks.Count != 0 && validRSSDataTasks != null)
               await Task.WhenAll(validRSSDataTasks);

            //foreach (var task in validRSSDataTasks)
            //{
            //    try 
            //    {
            //        var result = await Task.WhenAll(task);
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError($"Error Task TaskStatus: {task.Status} TaskException: {task.Exception} Exception: {ex.InnerException?.Message}");
            //    }
                
            //}

            var newsVMWithOutText = await GetNewsWithoutText();

            var newsesTestScrap =  newsVMWithOutText.Where(n => n.SourceUrl.Contains("belkagomel")).ToList();

            var tasksScrappingData = new List<Task>();
            foreach (var newsTestScrap in newsesTestScrap)
            {
                //var result = await UpdateNewsText(newsTestScrap);
                try
                {
                    tasksScrappingData.Add(UpdateNewsText(newsTestScrap));
                    //await UpdateNewsText(newsTestScrap);
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

            //var resultText5 = await UpdateNewsText(newsVMWithOutText.FirstOrDefault(n => n.SourceUrl.Contains("ctv.by")));

            await _appDBContext.Newses.FirstOrDefaultAsync();
          
        }



        private async Task GetRssData(Source source, List<string> existedNewsUrls)
        {
            if (source?.RssUrl != null)
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Async = true;
                using (var xmlreader = XmlReader.Create(source.RssUrl, settings))
                {
                    var syndicationFeed = SyndicationFeed.Load(xmlreader);
                    Console.WriteLine(source.BaseUrl);
                    var newses = syndicationFeed.Items
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
                   
                    //var uniqueNewses = newses.Where(n => !existedNewsUrls.Contains(n.SourceUrl)).ToList();

                    if (newses.Count != 0 && newses != null)
                    {
                        var newsesRSSData = _mapper.Map<List<News>>(newses.Where(vm => vm != null).Select(vm => vm));

                        await _appDBContext.Newses.AddRangeAsync(newsesRSSData);
                        await _appDBContext.SaveChangesAsync();
                    }
                }
            }
            
            
        }

        //private async Task GetRssData(Source source, List<string> existedNewsUrls)
        //{
        //    if (source?.RssUrl != null)
        //    {
        //        using (var xmlreader = XmlReader.Create(source.RssUrl))
        //        {
        //            var syndicationFeed = SyndicationFeed.Load(xmlreader);
        //            Console.WriteLine(source.BaseUrl);
        //            var newses = syndicationFeed.Items
        //                .Where(x => !existedNewsUrls.Contains(x.Links.FirstOrDefault().Uri.OriginalString))
        //                .Select(x => new NewsVM
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Title = x.Title.Text,
        //                    Description = x.Summary.Text,
        //                    Date = x.PublishDate.DateTime,
        //                    SourceUrl = x.Links.FirstOrDefault().Uri.OriginalString,
        //                    SourceId = source.Id,
        //                }).ToList();
        //            //var uniqueNewses = newses.Where(n => !existedNewsUrls.Contains(n.SourceUrl)).ToList();

        //            if (newses.Count != 0 && newses != null)
        //            {
        //                var newsesRSSData = _mapper.Map<List<News>>(newses.Where(vm => vm != null).Select(vm => vm));

        //                await _appDBContext.Newses.AddRangeAsync(newsesRSSData);
        //                await _appDBContext.SaveChangesAsync();
        //            }
        //        }
        //    }
        //}





        private async Task<List<NewsVM>> GetNewsWithoutText()
        {
            var newsVMWithOutText = _mapper.Map<List<NewsVM>>(await _appDBContext.Newses.Where(n => string.IsNullOrEmpty(n.Text)).Select(n => n).ToListAsync());

            return newsVMWithOutText;
        }

        private async Task UpdateNewsText(NewsVM newsVM)
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

                //var textBlocks = new List<HtmlNode>();
                //textBlocks = allChilNodes.Where(ch => ch.Name == "div").ToList();

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
                var newsNode = doc.DocumentNode.SelectSingleNode("//div[@class='news_img_slide']").InnerHtml;

                if (newsNode != null)
                {
                    var newsAllNode = doc.DocumentNode.SelectSingleNode("//div[@class='js-mediator-article']");
                    RemoveExcludeElementsBelta(newsAllNode);
                    newsNode += "\n";
                    newsNode += newsAllNode.InnerHtml;
                    
                        //newsNode = newsAllNode.DescendantsAndSelf()
                        //        .Where(node => !node.ChildNodes.Any(chn => chn.Name == "a")
                        //        && node.Name != "script"
                        //        && !node.Attributes["class"]?.Value?.Contains("yandex_share_print") 
                        //        && !node.Attributes["class"]?.Value?.Contains("advertising_block") 
                        //        && !node.Attributes["class"]?.Value?.Contains("news_tags_block") )
                        //    .Aggregate(string.Empty, (current, node) => current + node.OuterHtml);


                        //newsNode = newsAllNode.DescendantsAndSelf()
                        //        .Where(node => node.NodeType != HtmlNodeType.Element 
                        //        &&  !(node.Name == "div" 
                        //                && (node.Attributes["class"].Value.Contains("yandex_share_print")
                        //                || node.Attributes["class"].Value.Contains("advertising_block")
                        //                || node.Attributes["class"].Value.Contains("news_tags_block")) )
                        //        &&
                        //        !(node.Name == "div" && node.ChildNodes.Any(chn => chn.Name == "a")) 
                        //        && node.Name != "script")
                        //    .Aggregate(string.Empty, (current, node) => current + node.OuterHtml);
                    }
                else
                {
                    var newsAllNode = doc.DocumentNode.SelectSingleNode("//div[@class='js-mediator-article']");
                    RemoveExcludeElementsBelta(newsAllNode);
                    newsNode = newsAllNode.InnerHtml;
                   
                }
                news.Text = newsNode;

            }

            //not ready need last fixes
            if (newsVM.SourceUrl.Contains("sputnik.by"))
            {
                // непонятно почему не находит фотку, возможно следует искать по блокам выше( тпиа родительский div с определенным классом???)
                var newsNode = doc.DocumentNode.SelectSingleNode("//img[@media-type='ar16x9']").InnerHtml;
                if (newsNode != null)
                {
                    newsNode += "\n";
                    newsNode += doc.DocumentNode.SelectSingleNode("//div[@class='article__announce-text']").InnerHtml;
                }
                else
                {
                    newsNode += doc.DocumentNode.SelectSingleNode("//div[@class='article__announce-text']").InnerHtml;
                }

                if (newsNode != null)
                {
                    var newsAllNode = doc.DocumentNode.SelectSingleNode("//div[@class='article__body']");
                    //var textBlocks = newsAllNode.ChildNodes
                    //                    .Where(ch => ch.Name == "div" && ch.Attributes["class"]?.Value == "article__block" && (ch.Attributes["data-type"]?.Value == "text" || ch.Attributes["data-type"]?.Value == "quote")).ToList();
                    RemoveExcludeElementsSputnik(newsAllNode);
                    newsNode += "\n";
                    newsNode += newsAllNode.InnerHtml;
                    //var newsNodeWithoutATags = RemoveATags(newsNode);
                }
                else
                {
                    var newsAllNode = doc.DocumentNode.SelectSingleNode("//div[@class='article__body']");
                    //var textBlocks = newsAllNode.ChildNodes
                    //                    .Where(ch => ch.Name == "div" 
                    //                                && ch.Attributes["class"]?.Value == "article__block" 
                    //                                && (ch.Attributes["data-type"]?.Value == "text" 
                    //                                    || ch.Attributes["data-type"]?.Value == "quote"))
                    //                    .ToList();
                    RemoveExcludeElementsSputnik(newsAllNode);
                    newsNode = newsAllNode.InnerHtml;
                    //var newsNodeWithoutATags = RemoveATags(newsNode);
                }


                news.Text = RemoveATags(newsNode);

            }

            //not ready need last fixes
            if (newsVM.SourceUrl.Contains("belkagomel.by"))
            {
                var newsAllNodes = doc.DocumentNode.SelectSingleNode("//div[@class='entry']");
                
                RemoveExcludeElementsBelkagomel(newsAllNodes);

                //newsNode += "\n";
                //newsNode += doc.DocumentNode.SelectSingleNode("//div[@class='js-mediator-article']").InnerText;
                var newsNode = newsAllNodes.InnerHtml;
                news.Text = RemoveATags(newsNode);
            }


            //not ready maybe not gp.by отключен если чо, пока что временно смотри выше в фильтре source
            if (newsVM.SourceUrl.Contains("gp.by"))
            {
                var newsNode = doc.DocumentNode.SelectSingleNode("//article[@itemprop='description']").InnerHtml;
                //newsNode += "\n";
                //newsNode += doc.DocumentNode.SelectSingleNode("//div[@class='js-mediator-article']").InnerText;
                news.Text = newsNode;

            }


            // добавить обработку Html  всех оставшихся источников
            
            //news.Text = newsVM.Text;
        
            await _appDBContext.SaveChangesAsync();
        }
    }
}

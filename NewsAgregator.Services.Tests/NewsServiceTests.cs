using Microsoft.Identity.Client;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Data.Models;
using NewsAgregator.Services.NewsServices;
using NewsAgregator.ViewModels.Data;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Serilog;

namespace NewsAgregator.Services.Tests
{
    public class NewsServiceTests
    {
        private readonly INewsServices _newsService;
        private readonly List<NewsVM> _testNewsesRating;
        private readonly List<NewsVM> _testNewsesDate;

        public NewsServiceTests()
        {

            var currentDate = DateTime.Now;
            _testNewsesDate = new List<NewsVM>
            {
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(-15) },
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(-12) },
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(-17) },
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(-20) },
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(-13) },
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(-10) }
            };

            _testNewsesRating = new List<NewsVM>
            {
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 3f },
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 2f },
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 5f },
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 6f },
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 7f },
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 1f }
            };

            
           
            
        }

        [Fact]
        public async Task DeleteNewsWithBadRateAsync_DeletesNewsWithRatingLessThanOrEqualTo4()
        {
            // Arrange
            var newsServiceMock = Substitute.For<INewsServices>();

            var testNews = _testNewsesRating;
            newsServiceMock.TakeNewsesAsync().Returns(Task.FromResult(testNews));

            int callDeleteBadRateCount = 0;
            newsServiceMock.When(n => n.DeleteNewsAsync(Arg.Any<Guid>())).Do(n =>
            {
                callDeleteBadRateCount++;
            });
           
            var nsts = new NewsScheduledTasksServices(newsServiceMock);

            // Act
            await nsts.DeleteNewsWithBadRateAsync();

            // Assert
            var newsCount = testNews.Count(tn => tn.PositiveRating <=4);
            Assert.Equal(newsCount, callDeleteBadRateCount); 

        }


        [Fact]
        public async Task DeleteOldNewsesAsync_DeletesNewsOlderThanTwoWeeks()
        {
            // Arrange
            var newsServiceMock = Substitute.For<INewsServices>();

            var testNews = _testNewsesDate;
            newsServiceMock.TakeNewsesAsync().Returns(Task.FromResult(testNews));

            int callDeleteOldDateCount = 0;
            newsServiceMock.When(n => n.DeleteNewsAsync(Arg.Any<Guid>())).Do(n =>
            {
                callDeleteOldDateCount++;
            });

            var nsts = new NewsScheduledTasksServices(newsServiceMock);

            // Act
            await nsts.DeleteOldNewsesAsync();

            // Assert
            var newsCount = testNews.Count(tn => tn.Date.Value <= DateTime.Now.AddDays(-14));
            Assert.Equal(newsCount, callDeleteOldDateCount); 

        }
    }


    
}
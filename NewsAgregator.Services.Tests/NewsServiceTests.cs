using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Data.Models;
using NewsAgregator.Services.NewsServices;
using NewsAgregator.ViewModels.Data;
using NSubstitute;

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
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(-13) },
                new NewsVM { Id = Guid.NewGuid(), Date = currentDate.AddDays(1) }
            };

            _testNewsesRating = new List<NewsVM>
            {
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 3f },
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 2f },
                new NewsVM { Id = Guid.NewGuid(), PositiveRating = 5f }
            };

            _newsService = Substitute.For<INewsServices>();
            _newsService.TakeNewsesAsync().Returns(_testNewsesRating);
            
        }

        [Fact]
        public async Task DeleteNewsWithBadRateAsync_DeletesNewsWithRatingLessThanOrEqualTo4()
        {
            // Arrange
            var newsService = _newsService;
            _newsService.When(n => n.DeleteNewsAsync(Arg.Any<Guid>()))
                    .Do(info => _testNewsesRating.RemoveAll(n => n.Id == Arg.Any<Guid>()));

            // Act
            await newsService.DeleteNewsWithBadRateAsync();

            // Assert
            var deletedNewsCount = _testNewsesRating.Count(n => n.PositiveRating <= 4);
            Assert.Equal(2, deletedNewsCount);

        }


        [Fact]
        public async Task DeleteOldNewsesAsync_DeletesNewsOlderThanTwoWeeks()
        {
            // Arrange
            var newsService = _newsService;
            _newsService.When(n => n.DeleteNewsAsync(Arg.Any<Guid>()))
                   .Do(info => _testNewsesDate.RemoveAll(n => n.Id == Arg.Any<Guid>()));

            // Act
            await newsService.DeleteOldNewsesAsync();

            // Assert
            var deletedNewsCount = _testNewsesDate.Count(n => n.Date.Value <= DateTime.Now.AddDays(-14));
            Assert.Equal(1, deletedNewsCount);
            Assert.DoesNotContain(_testNewsesDate, n => n.Date.Value <= DateTime.Now.AddDays(-14));
        }
    }


    
}
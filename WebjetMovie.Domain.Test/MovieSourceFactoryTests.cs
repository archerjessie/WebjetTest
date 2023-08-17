using System;
using Microsoft.Extensions.Logging;
using Moq;
using WebjetMovie.Domain.DataSources;
using WebjetMovie.Domain.Models;
using Xunit;

namespace WebjetMovie.Domain.Test
{
    public class MovieSourceFactoryTests
    {
        [Fact]
        public void GetMovieSource_ReturnsMovieSourceInstance()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<MovieSource>>();
            var factory = new MovieSourceFactory(mockLogger.Object);

            // Act
            var result = factory.GetMovieSource(ESource.CINEMAWORLD);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MovieSource>(result);
        }
    }
}

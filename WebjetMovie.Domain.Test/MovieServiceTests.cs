using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using WebjetMovie.Domain.DataSources;
using WebjetMovie.Domain.Models;
using WebjetMovie.Domain.Services;
using Xunit;
namespace WebjetMovie.Domain.Test
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task GetMovies_ReturnsCombinedListOfMoviesFromSources()
        {
            // Arrange
            var mockMovieSourceFactory = new Mock<IMovieSourceFactory>();
            var mockCinemaworldSource = new Mock<IMovieSource>();
            var mockFilmworldSource = new Mock<IMovieSource>();

            var cinemaworldMovies = new MovieList
            {
                Movies = new List<MovieSummary>
                {
                    new MovieSummary { Title = "Movie A", ID = "123" },
                    new MovieSummary { Title = "Movie B", ID = "456" }
                }
            };

            var filmworldMovies = new MovieList
            {
                Movies = new List<MovieSummary>
                {
                    new MovieSummary { Title = "Movie B", ID = "789" },
                    new MovieSummary { Title = "Movie C", ID = "1011" }
                }
            };

            mockCinemaworldSource.Setup(source => source.GetMoviesAsync()).ReturnsAsync(cinemaworldMovies);
            mockFilmworldSource.Setup(source => source.GetMoviesAsync()).ReturnsAsync(filmworldMovies);

            mockMovieSourceFactory.Setup(factory => factory.GetMovieSource(ESource.CINEMAWORLD))
                .Returns(mockCinemaworldSource.Object);

            mockMovieSourceFactory.Setup(factory => factory.GetMovieSource(ESource.FILMWORLD))
                .Returns(mockFilmworldSource.Object);

            var movieService = new MovieService(mockMovieSourceFactory.Object);

            // Act
            var result = await movieService.GetMovies();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(result, movie => movie.Title == "Movie A");
            Assert.Contains(result, movie => movie.Title == "Movie B");
            Assert.Contains(result, movie => movie.Title == "Movie C");
        }

        [Fact]
        public async Task GetMovieDetail_ReturnsCombinedMovieDetailFromSources()
        {
            // Arrange
            var mockMovieSourceFactory = new Mock<IMovieSourceFactory>();
            var mockCinemaworldSource = new Mock<IMovieSource>();
            var mockFilmworldSource = new Mock<IMovieSource>();

            var cinemaworldMovieDetail = new MovieDetail
            {
                Title = "Movie A",
                Price = 9.99M,
                Year = "2020",
                Genre = "Action",
                Director = "Director A",
                Actors = "Actor A",
                Language = "English"
            };

            var filmworldMovieDetail = new MovieDetail
            {
                Title = "Movie A",
                Price = 8.99M,
                Year = "2020",
                Genre = "Action",
                Director = "Director B",
                Actors = "Actor B",
                Language = "English"
            };

            mockCinemaworldSource.Setup(source => source.GetMovieDetailAsync("123"))
                .ReturnsAsync(cinemaworldMovieDetail);

            mockFilmworldSource.Setup(source => source.GetMovieDetailAsync("456"))
                .ReturnsAsync(filmworldMovieDetail);

            mockMovieSourceFactory.Setup(factory => factory.GetMovieSource(ESource.CINEMAWORLD))
                .Returns(mockCinemaworldSource.Object);

            mockMovieSourceFactory.Setup(factory => factory.GetMovieSource(ESource.FILMWORLD))
                .Returns(mockFilmworldSource.Object);

            var movieService = new MovieService(mockMovieSourceFactory.Object);

            // Act
            var result = await movieService.GetMovieDetail("123", "456");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Movie A", result.Title);
            Assert.Equal(8.99M, result.Price);
            Assert.Equal("Director B", result.Director);
        }
    }
}


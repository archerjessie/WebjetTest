using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using WebjetMovie.api.Controllers;
using WebjetMovie.Domain.Dtos;
using WebjetMovie.Domain.Services;
using Xunit;

namespace WebjetMovie.Api.Test
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task Get_ReturnsListOfMovies()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MoviesController(mockMovieService.Object);

            var movies = new List<MovieDto>
            {
                new MovieDto { Title = "Movie A", CinemaworldId = "123", FilmworldId = "456" },
                new MovieDto { Title = "Movie B", CinemaworldId = "789", FilmworldId = "1011" }
            };

            mockMovieService.Setup(service => service.GetMovies()).ReturnsAsync(movies);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, movie => movie.Title == "Movie A");
            Assert.Contains(result, movie => movie.Title == "Movie B");
        }

        [Fact]
        public async Task GetDetail_ReturnsMovieDetail()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MoviesController(mockMovieService.Object);

            var movieDetail = new MovieDetailDto
            {
                Title = "Movie A",
                Director = "Director A",
                Price = 9.99M
            };

            mockMovieService.Setup(service => service.GetMovieDetail("123", "456")).ReturnsAsync(movieDetail);

            // Act
            var result = await controller.GetDetail("123", "456");

            // Assert
            Assert.Equal("Movie A", result.Title);
            Assert.Equal("Director A", result.Director);
            Assert.Equal(9.99M, result.Price);
        }
    }
}

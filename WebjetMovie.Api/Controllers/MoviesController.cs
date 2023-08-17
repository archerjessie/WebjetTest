using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebjetMovie.Domain.Dtos;
using WebjetMovie.Domain.Services;

namespace WebjetMovie.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> Get()
        {
            var movies = await _movieService.GetMovies();
            return movies;
        }

        [HttpGet("detail")]
        public async Task<MovieDetailDto> GetDetail([FromQuery] string cwid, [FromQuery] string fwid)
        {
            var movieDetail = await _movieService.GetMovieDetail(cwid, fwid);
            return movieDetail;
        }
    }
}

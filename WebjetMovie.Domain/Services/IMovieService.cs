using System.Collections.Generic;
using System.Threading.Tasks;
using WebjetMovie.Domain.Dtos;

namespace WebjetMovie.Domain.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetMovies();
        Task<MovieDetailDto> GetMovieDetail(string cinemaworldId, string filmworldId);
    }
}

using System.Threading.Tasks;
using WebjetMovie.Domain.Models;

namespace WebjetMovie.Domain.DataSources
{
    public interface IMovieSource
    {
        Task<MovieList> GetMoviesAsync();
        Task<MovieDetail> GetMovieDetailAsync(string id);
    }
}

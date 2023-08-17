using WebjetMovie.Domain.Models;

namespace WebjetMovie.Domain.DataSources
{
    public interface IMovieSourceFactory
    {
        IMovieSource GetMovieSource(ESource source);
    }
}

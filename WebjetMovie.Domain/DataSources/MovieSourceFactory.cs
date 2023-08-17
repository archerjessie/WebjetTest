using Microsoft.Extensions.Logging;
using WebjetMovie.Domain.Models;

namespace WebjetMovie.Domain.DataSources
{
    public class MovieSourceFactory : IMovieSourceFactory
    {
        private readonly ILogger<MovieSource> _logger;

        public MovieSourceFactory(ILogger<MovieSource> logger)
        {
            _logger = logger;
        }

        public IMovieSource GetMovieSource(ESource source)
        {
            return new MovieSource(source, _logger);
        }
    }
}

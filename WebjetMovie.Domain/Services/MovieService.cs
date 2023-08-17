using System.Collections.Generic;
using System.Threading.Tasks;
using WebjetMovie.Domain.DataSources;
using WebjetMovie.Domain.Dtos;
using WebjetMovie.Domain.Models;

namespace WebjetMovie.Domain.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieSourceFactory _movieSourceFactory;

        public MovieService(IMovieSourceFactory movieSourceFactory)
        {
            _movieSourceFactory = movieSourceFactory;
        }

        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            IMovieSource cinemaworldDataSource = _movieSourceFactory.GetMovieSource(ESource.CINEMAWORLD);
            IMovieSource filmworldDataSource = _movieSourceFactory.GetMovieSource(ESource.FILMWORLD);

            Task<MovieList> getFromCinemaworld = cinemaworldDataSource.GetMoviesAsync();
            Task<MovieList> getFromFilmworld = filmworldDataSource.GetMoviesAsync();

            await Task.WhenAll(getFromCinemaworld, getFromFilmworld);

            MovieList fromCinemaworld = getFromCinemaworld.Result;
            MovieList fromFilmworld = getFromFilmworld.Result;

            IDictionary<string, MovieDto> movies = new Dictionary<string, MovieDto>();
            AddMovieToDictionary(movies, fromCinemaworld, ESource.CINEMAWORLD);
            AddMovieToDictionary(movies, fromFilmworld, ESource.FILMWORLD);

            return movies.Values;
        }

        public async Task<MovieDetailDto> GetMovieDetail(string cinemaworldId, string filmworldId)
        {
            IMovieSource cinemaworldDataSource = _movieSourceFactory.GetMovieSource(ESource.CINEMAWORLD);
            IMovieSource filmworldDataSource = _movieSourceFactory.GetMovieSource(ESource.FILMWORLD);

            Task<MovieDetail> getFromCinemaworld = cinemaworldDataSource.GetMovieDetailAsync(cinemaworldId);
            Task<MovieDetail> getFromFilmworld = filmworldDataSource.GetMovieDetailAsync(filmworldId);

            await Task.WhenAll(getFromCinemaworld, getFromFilmworld);

            MovieDetail fromCinemaworld = getFromCinemaworld.Result;
            MovieDetail fromFilmworld = getFromFilmworld.Result;

            if(fromCinemaworld == null && fromFilmworld == null)
            {
                return null;
            }

            if(fromCinemaworld == null)  // cinemaworldId not exist or data not availble for cw
            {
                return toMovieDetailDto(fromFilmworld);
            }

            if(fromFilmworld == null)
            {
                return toMovieDetailDto(fromCinemaworld);
            }

            return fromCinemaworld.Price <= fromFilmworld.Price ?
                toMovieDetailDto(fromCinemaworld) : toMovieDetailDto(fromFilmworld);
        }

        private MovieDetailDto toMovieDetailDto(MovieDetail movieDetail)
        {
            return new MovieDetailDto()
            {
                Title = movieDetail.Title,
                Price = movieDetail.Price,
                Year = movieDetail.Year,
                Genre = movieDetail.Genre,
                Director = movieDetail.Director,
                Actors = movieDetail.Actors,
                Language = movieDetail.Language
            };
        }

        private void AddMovieToDictionary(IDictionary<string, MovieDto> movieDictionary,
            MovieList movieCollection, ESource source)
        {
            if (movieCollection == null)
            {
                return;
            }
                
            foreach (var movie in movieCollection.Movies)
            {
                if (!movieDictionary.ContainsKey(movie.Title))
                {
                    movieDictionary.Add(movie.Title,
                        new MovieDto()
                        {
                            Title = movie.Title,
                            Poster = movie.Poster
                        });
                }

                switch (source)
                {
                    case ESource.CINEMAWORLD:
                        movieDictionary[movie.Title].CinemaworldId = movie.ID;
                        break;
                    case ESource.FILMWORLD:
                        movieDictionary[movie.Title].FilmworldId = movie.ID;
                        break;
                }
            }
        }
    }
}

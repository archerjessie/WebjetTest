using System;
using System.Collections.Generic;

namespace WebjetMovie.Domain.Models
{
    public class MovieList
    {
        public IEnumerable<MovieSummary> Movies { get; set; }
    }
}

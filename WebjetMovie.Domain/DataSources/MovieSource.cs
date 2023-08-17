using System;
using System.Collections.Generic;
using WebjetMovie.Domain.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace WebjetMovie.Domain.DataSources
{
    public class MovieSource : IMovieSource
    {
        private const string baseUrl = "http://webjetapitest.azurewebsites.net";
        private const string accessToken = "sjd1HfkjU83ksdsm3802k"; // in a real world application, we shouldn't hard code the token here
        private readonly ESource _dataSource;
        private readonly ILogger<MovieSource> _logger;

        public MovieSource(ESource dataSource, ILogger<MovieSource> logger)
        {
            _dataSource = dataSource;
            _logger = logger;
        }

        public async Task<MovieDetail> GetMovieDetailAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return null;
            }

            string url = $"{baseUrl}/api/{_dataSource}/movie/{id}";
            return await GetDataAsync<MovieDetail>(url);
        }

        public async Task<MovieList> GetMoviesAsync()
        {
            string url = $"{baseUrl}/api/{_dataSource}/movies";
            return await GetDataAsync<MovieList>(url);
        }

        private async Task<T> GetDataAsync<T>(string url) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                client.DefaultRequestHeaders.Add("x-access-token", accessToken);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonPayload = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(jsonPayload);
                    }
                    else
                    {
                        _logger.LogWarning($"API call to {url} failed, HTTP Status Code: {response.StatusCode}");
                        return null;
                    }
                }
                catch (TaskCanceledException)
                {
                    _logger.LogWarning($"API call to {url} failed, request timed out.");
                    return null;
                }
            }
        }
    }
}

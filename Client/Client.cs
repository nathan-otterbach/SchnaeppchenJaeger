#pragma warning disable CS8618

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System.Threading.Tasks;
using System.Threading;

namespace SchnaeppchenJaeger.Client
{
    internal class Client : IDisposable
    {
        private bool _disposed = false;
        private readonly HttpClient _httpClient;
        private AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        private uint _zipCode;
        private string _querySearch;
        private string _baseUrl = "https://api.marktguru.de/api/v1/offers/search?as=web&";
        private const string _apiKey = "8Kk+pmbf7TgJ9nVj2cXeA7P5zBGv8iuutVVMRfOfvNE=";

        public Client()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-Apikey", _apiKey);

            // Configure retry policy using Polly
            _retryPolicy = Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
                                 .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5),
                                 (response, timeSpan, retryCount, context) => { });
        }

        public Client(uint zipCode, string querySearch) : this()
        {
            ZipCode = zipCode;
            QuerySearch = querySearch;
        }

        public async Task<string> GetOffersAsync(CancellationToken cancellationToken)
        {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"{_baseUrl}&q={QuerySearch}&zipCode={ZipCode}", cancellationToken));

            response.EnsureSuccessStatusCode();

            // get properties we need

            return null;
        }

        public uint ZipCode
        {
            get { return _zipCode; }
            set
            {
                // Check if value is within the valid range and has 5 digits
                if (value < 1067 ||
                    value > 99998 ||
                    value.ToString().Length != 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Zip code must be a 5-digit number between 01067 and 99998.");
                }

                // Ensure that value stays within the bounds of a uint
                checked
                {
                    _zipCode = value;
                }
            }
        }

        public string QuerySearch
        {
            get { return _querySearch; }
            set
            {
                // Check if value is not null or empty
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value), "Query search must not be null or empty.");
                }

                _querySearch = Uri.EscapeDataString(value);
            }
        }

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
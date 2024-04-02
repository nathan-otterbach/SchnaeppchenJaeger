#pragma warning disable CS8618

using System.Configuration;
using System.Net.Http.Headers;
using Polly;
using Polly.Retry;

namespace SchnaeppchenJaeger.Client
{
    public class ApiClient : IDisposable
    {
        private bool _disposed = false;
        private readonly HttpClient _httpClient;
        private AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private Utils.Utils _utils;

        private uint _zipCode;
        private string _querySearch;

        public enum Status
        {
            Success,
            Failure
        }

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Apikey", ConfigurationManager.AppSettings["ApiKey"]);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Configure retry policy using Polly
            _retryPolicy = Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
                                 .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5),
                                 (response, timeSpan, retryCount, context) => { });
            _utils = new Utils.Utils();
        }

        public ApiClient(uint zipCode, string querySearch) : this()
        {
            ZipCode = zipCode;
            QuerySearch = querySearch;
        }

        public async Task<Status> GetOffersAsync(CancellationToken cancellationToken)
        {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"{ConfigurationManager.AppSettings["BaseUrl"]}q={QuerySearch}&zipCode={ZipCode}", cancellationToken));

            response.EnsureSuccessStatusCode();
            cancellationToken.ThrowIfCancellationRequested();

            if (!_utils.GetPropertiesFromResponse(response).Any())
            {
                return Status.Failure;
            }

            return Status.Success;
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
        
        ~ApiClient() { Dispose(false); }

        #endregion
    }
}

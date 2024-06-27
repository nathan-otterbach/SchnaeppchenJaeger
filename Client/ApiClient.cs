#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Configuration;
using System.Net.Http.Headers;
using Polly;
using Polly.Retry;

namespace SchnaeppchenJaeger.Client
{
    /// <summary>
    /// Represents a client for interacting with a remote API.
    /// </summary>
    public class ApiClient : IDisposable
    {
        private bool _disposed = false;
        private readonly HttpClient _httpClient;
        private AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        private uint _zipCode;
        private string _querySearch;

        /// <summary>
        /// Enum representing the status of an API operation.
        /// </summary>
        public enum Status
        {
            Success,
            Cancelled,
            Failure
        }

        /// <summary>
        /// Default constructor for ApiClient.
        /// </summary>
        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Apikey", ConfigurationManager.AppSettings["ApiKey"]);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _retryPolicy = Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
                                 .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5),
                                 (response, timeSpan, retryCount, context) => { });
        }

        /// <summary>
        /// Constructor for ApiClient with parameters.
        /// </summary>
        /// <param name="zipCode">The zip code to use in API requests.</param>
        /// <param name="querySearch">The search query to use in API requests.</param>
        public ApiClient(uint zipCode, string querySearch) : this()
        {
            ZipCode = zipCode;
            QuerySearch = querySearch;
        }

        /// <summary>
        /// Asynchronously retrieves offers from the API.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for cancelling the operation.</param>
        /// <returns>Status indicating whether the operation was successful or not.</returns>
        public async Task<Status> GetOffersAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"{ConfigurationManager.AppSettings["BaseUrl"]}q={QuerySearch}&zipCode={ZipCode}", cancellationToken));

            response.EnsureSuccessStatusCode();

            if (!Program._utils.GetPropertiesFromResponse(response).Any())
            {
                return Status.Failure;
            }

            return Status.Success;
        }


        /// <summary>
        /// Gets or sets the zip code to use in API requests.
        /// </summary>
        public uint ZipCode
        {
            get { return _zipCode; }
            set
            {
                if (value < 1067 ||
                    value > 99998 ||
                    value.ToString().Length != 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Zip code must be a 5-digit number between 01067 and 99998.");
                }

                _zipCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the search query to use in API requests.
        /// </summary>
        public string QuerySearch
        {
            get { return _querySearch; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value), "Query search must not be null or empty.");
                }

                _querySearch = Uri.EscapeDataString(value);
            }
        }

        #region IDisposable implementation

        /// <summary>
        /// Disposes of resources used by the ApiClient.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of resources used by the ApiClient.
        /// </summary>
        /// <param name="disposing">Indicates if disposing is in progress.</param>
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

        /// <summary>
        /// Finalizer for the ApiClient class.
        /// </summary>
        ~ApiClient() { Dispose(false); }

        #endregion
    }
}
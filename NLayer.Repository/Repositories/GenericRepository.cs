using Newtonsoft.Json;
using NLayer.Core.Repositories;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HttpClient client;
        public GenericRepository()
        {
            client = new HttpClient();
        }

        public Uri BuildRequestUri(string apiURL, string appId, NameValueCollection? queryParameters = null)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            if (queryParameters != null)
            {
                parameters.Add(queryParameters);
            }

            parameters.Add("APPID", appId);               

            var query = parameters.ToString();
            var requestUri = new Uri(apiURL);
            var builder = new UriBuilder(requestUri) { Query = query };
            return builder.Uri;
        }

        public async Task<T?> SendAsync(HttpMethod httpMethod, Uri uri, HttpContent? httpContent = null)
        {
            var request = new HttpRequestMessage(httpMethod, uri) { Content = httpContent };
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.Append("Request Error:");
                messageBuilder.AppendFormat("\n- Got status {0} ({1}), expected: 2xx.", response.StatusCode, (int)response.StatusCode);
                messageBuilder.AppendFormat("\n- Request Status Line: {0} {1}.", httpMethod.Method, uri);
                messageBuilder.Append("\n- Response Headers:");

                foreach (var header in response.Headers)
                {
                    messageBuilder.AppendFormat("\n    - {0}:\t{1}", header.Key, string.Join(", ", header.Value));
                }

                messageBuilder.AppendFormat("\n- Response Content: {0}", content);
                throw new InvalidOperationException(messageBuilder.ToString());
            }

            return JsonConvert.DeserializeObject<T?>(content);
        }

        public string BuildRequestCacheKey(NameValueCollection? parameters)
        {
            if (parameters is null)
            {
                return $"req-";
            }

            var combinedParameters = parameters.Keys
                .Cast<string>()
                .Select(s => s + ":" + parameters[s]);

            return $"req-{{{string.Join(";", combinedParameters)}}}";
        }
    }
}

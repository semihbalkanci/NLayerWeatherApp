using System.Collections.Specialized;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Uri BuildRequestUri(string apiURL, string appId , NameValueCollection? queryParameters = null);
        public Task<T?> SendAsync(HttpMethod httpMethod, Uri uri, HttpContent? httpContent = null);
    }
}

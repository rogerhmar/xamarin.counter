using System.Net.Http;
using System.Threading.Tasks;

namespace xamarin.counter
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer");
     }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xamarin.game;
using xamarin.game.HttpClients;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService))]
namespace xamarin.game
{
    class DataService : IDataService
    {
        private IHttpClient _httpClient;

        public DataService()
        {
            _httpClient = new ResilientHttpClient();
        }

        private const string localHost = "10.0.2.2";
        private const string Uri = "http://"+localHost+":5000/api/values/";
        
        public async Task<string> GetNextCounterAsync(int counter)
        {
            var msg = $"{Uri}{counter}";
            return await _httpClient.GetStringAsync(msg);
        }

    }
}

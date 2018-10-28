using System.Threading.Tasks;
using xamarin.counter;

namespace xamarin.game.tests
{
    public class DataServiceMock : IDataService
    {
        public Task<string> GetNextCounterAsync(int counter)
        {
            return Task.FromResult((counter + 1).ToString());
        }
    }
}
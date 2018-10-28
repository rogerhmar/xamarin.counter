using System.Threading.Tasks;

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
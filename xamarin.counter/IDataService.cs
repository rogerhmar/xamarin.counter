using System.Threading.Tasks;

namespace xamarin.counter
{
    public interface IDataService
    {
        Task<string> GetNextCounterAsync(int counter);
    }
}
using System.Threading.Tasks;

namespace xamarin.game
{
    public interface IDataService
    {
        Task<string> GetNextCounterAsync(int counter);
    }
}
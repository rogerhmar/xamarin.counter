using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace xamarin.counter
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand ButtonClicked { get; }
        public int Count { get; set; }
        public string Status { get; set; } = "";

        private IDataService dataService => DependencyService.Get<IDataService>();
        
        public MainViewModel()
        {
            ButtonClicked = new AsyncDelegateCommand(AsyncExecute);
        }

        private async Task AsyncExecute(object arg)
        {
            var result = await dataService.GetNextCounterAsync(Count);
            if (int.TryParse(result, out var content))
            {
                Count = content;
                Status = "OK";
            }
            else
            {
                Status = result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

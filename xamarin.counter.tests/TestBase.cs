using xamarin.counter;
using Xamarin.Forms;

namespace xamarin.game.tests
{
    public class TestBase
    {
        public TestBase()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            DependencyService.Register<IDataService, DataServiceMock>();

        }
    }
}
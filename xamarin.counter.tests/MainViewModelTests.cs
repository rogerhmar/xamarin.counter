using FluentAssertions;
using NUnit.Framework;
using xamarin.counter;

namespace xamarin.game.tests
{
    public class MainViewModelTests : TestBase
    {
        [Test]
        public void CounterIsUpdatedAsExpected()
        {
            var vm = new MainViewModel();
            vm.ButtonClicked.Execute(null);
            vm.ButtonClicked.Execute(null);
            vm.Count.Should().Be(2);
        }
    }
}

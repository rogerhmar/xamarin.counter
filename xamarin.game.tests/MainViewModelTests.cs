using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Moq;
using NUnit.Framework;

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

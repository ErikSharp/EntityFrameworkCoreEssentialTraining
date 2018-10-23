using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataAccessLayerTests
{
    public class MyFirstTestFixture : IDisposable
    {
        private int _counter;
        private string _phrase;
        public MyFirstTestFixture()
        {
            _counter = 0;
            _phrase = "hello";
        }

        public void Dispose()
        {
        }

        [Fact]
        public void ShouldEqualZero()
        {
            Assert.Equal(0, _counter);
            Assert.Equal("hello", _phrase);
            Assert.Equal("Hello", _phrase, ignoreCase: true);
        }
    }
}

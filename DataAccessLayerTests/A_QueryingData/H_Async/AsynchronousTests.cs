using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.H_Async
{
    public class AsynchronousTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public AsynchronousTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async void ShouldAwaitAsyncQueries()
        {
        }
    }
}

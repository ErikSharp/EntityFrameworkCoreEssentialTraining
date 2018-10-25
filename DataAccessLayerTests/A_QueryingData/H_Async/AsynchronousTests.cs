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
            //remember that async doesn't speed up the query execution
            //it just prevents your other operations from being blocked

            //EF Core doesn't support multiple open operations on a context

            var prodList = await _context.Product.ToListAsync();
            var product = await _context.Product.FindAsync(3);
        }
    }
}

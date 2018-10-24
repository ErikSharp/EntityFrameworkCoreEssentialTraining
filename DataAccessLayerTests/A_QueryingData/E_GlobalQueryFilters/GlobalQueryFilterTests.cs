using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.E_GlobalQueryFilters
{
    public class GlobalQueryFilterTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public GlobalQueryFilterTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldNotBringRecordsBackWithSellEndDate()
        {
            var prodList = _context.Product.ToList();
            Assert.Equal(406, prodList.Count);
        }

        [Fact]
        public void ShouldBringAllRecordsBack()
        {
            var prodList = _context.Product.IgnoreQueryFilters().ToList();
            Assert.Equal(504, prodList.Count);
        }
    }
}

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
        //TODO: Add QueryFilter to AdventureWorksContext
        [Fact]
        public void ShouldNotBringRecordsBackWithSellEndDate()
        {
        }
        [Fact]
        public void ShouldBringAllRecordsBack()
        {
        }
    }
}

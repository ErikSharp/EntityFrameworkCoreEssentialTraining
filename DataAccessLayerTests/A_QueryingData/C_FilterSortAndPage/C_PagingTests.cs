using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.C_FilterSortAndPage
{
    public class PagingTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public PagingTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldSkipFirst25Records()
        {
        }
        [Fact]
        public void ShouldTakeFirst25Records()
        {
        }
        [Fact]
        public void ShouldSkip25ThenTakeFirst25Records()
        {
        }

        [Fact(Skip="SkipWhile and TakeWhile are not supported by EF Core")]
        public void ShouldSkipWhile()
        {
        }
    }
}
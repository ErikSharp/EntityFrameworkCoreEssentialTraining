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
            var prodList = _context.Product
                .Where(p => p.SellEndDate == null)
                .OrderBy(p => p.ProductId)
                .Skip(25).ToList();
            Assert.Equal(381, prodList.Count);
            Assert.Equal(345, prodList[0].ProductId);
        }

        [Fact]
        public void ShouldTakeFirst25Records()
        {
            var prodList = _context.Product
                .Where(p => p.SellEndDate == null)
                .OrderBy(p => p.ProductId)
                .Take(25).ToList();

            Assert.Equal(25, prodList.Count);
            Assert.Equal(1, prodList[0].ProductId);
        }
        [Fact]
        public void ShouldSkip25ThenTakeFirst25Records()
        {
            var prodList = _context.Product
                .Where(p => p.SellEndDate == null)
                .OrderBy(p => p.ProductId)
                .Skip(25).Take(25).ToList();

            Assert.Equal(25, prodList.Count);
            Assert.Equal(345, prodList[0].ProductId);
        }

        [Fact(Skip="SkipWhile and TakeWhile are not supported by EF Core")]
        public void ShouldSkipWhile()
        {
        }
    }
}
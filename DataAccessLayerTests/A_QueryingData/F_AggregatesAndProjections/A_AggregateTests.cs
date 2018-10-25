using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.F_AggregatesAndProjections
{
    public class AggregateTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public AggregateTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldCalculateSumOfPrices()
        {
            decimal sum = _context.Product.Sum(p => p.ListPrice);

            Assert.Equal(130875.72m, sum);
        }

        [Fact]
        public void ShouldCalculateCountOfNonZeroPrices()
        {
            int count = _context.Product.Count(p => p.ListPrice != 0);
            Assert.Equal(206, count);
        }

        [Fact]
        public void ShouldCalculateTheAverageListPriceOfNonZeroPrices()
        {
            //decimal avg = _context.Product
            //    .Where(p => p.ListPrice != 0)
            //    .Average(p => p.ListPrice);

            //Convert to a decimal so EF thinks it's a server side execution
            decimal avg = _context.Product
                .Where(p => p.ListPrice != 0)
                .Average(p => (decimal?)p.ListPrice) ?? 0;

            Assert.Equal(635.319029m, avg);
        }

        [Fact]
        public void ShouldCalculateTheMaxListPrice()
        {
            //Convert to a decimal so EF thinks it's a server side execution
            decimal max = _context.Product
                .Where(p => p.ListPrice != 0)
                .Max(p => (decimal?)p.ListPrice) ?? 0;

            Assert.Equal(2443.35m, max);
        }

        [Fact]
        public void ShouldCalculateTheMinListPriceOrNonZeroPrices()
        {
            //Convert to a decimal so EF thinks it's a server side execution
            decimal min = _context.Product
                .Where(p => p.ListPrice != 0)
                .Min(p => (decimal?)p.ListPrice) ?? 0;

            Assert.Equal(2.29m, min);
        }

        [Fact]
        public void ShouldDetermineIfAnyExistWithListPriceNotZero()
        {
            bool any = _context.Product.Any(p => p.ListPrice != 0);
            Assert.True(any);
        }

        [Fact]
        public void ShouldDetermineIfAllHaveListPriceNotZero()
        {
            bool all = _context.Product.All(p => p.ListPrice != 0);
            Assert.False(all);
        }
    }
}
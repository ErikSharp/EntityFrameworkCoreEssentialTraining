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
        }
        [Fact]
        public void ShouldCalculateCountOfNonZeroPrices()
        {
        }

        [Fact]
        public void ShouldCalculateTheAverageListPriceOfNonZeroPrices()
        {
        }

        [Fact]
        public void ShouldCalculateTheMaxListPrice()
        {
        }

        [Fact]
        public void ShouldCalculateTheMinListPriceOrNonZeroPrices()
        {
        }

        [Fact]
        public void ShouldDetermineIfAnyExistWithListPriceNotZero()
        {
        }

        [Fact]
        public void ShouldDetermineIfAllHaveListPriceNotZero()
        {
        }
    }
}
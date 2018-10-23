using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.G_RelatedData
{
    public class ExplicitlyLoadRelatedDataTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ExplicitlyLoadRelatedDataTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldEagerlyLoadRelatedData()
        {
        }

        [Fact]
        public void ShouldEagerlyLoadMultipleLevelsRelatedData()
        {
        }

        [Fact]
        public void ShouldQueryRelatedData()
        {
        }

    }
}
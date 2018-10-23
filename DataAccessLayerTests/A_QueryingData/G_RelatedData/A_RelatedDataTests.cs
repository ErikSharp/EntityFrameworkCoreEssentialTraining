using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.G_RelatedData
{
    public class EagerLoadRelatedDataTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public EagerLoadRelatedDataTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldNotLoadRelatedDataByDefault()
        {
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
        public void ShouldIgnoreIncludeWithProjections()
        {
        }

        [Fact]
        public void ShouldReattachRelatedEntries()
        {
        }
    }
}

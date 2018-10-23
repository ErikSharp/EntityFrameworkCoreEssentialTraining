using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.ViewModels;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.F_AggregatesAndProjections
{
    public class ProjectionTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ProjectionTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldCreateNewAnonymousObjects()
        {
        }

        //TODO: Copy in ProductViewModel from Assets folder
        [Fact]
        public void ShouldCreateNewViewModels()
        {
        }
    }
}

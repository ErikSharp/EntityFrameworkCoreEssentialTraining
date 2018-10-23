using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.C_Tracking
{
    public class TrackingTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public TrackingTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldTrackChangesByDefault()
        {
        }

        [Fact]
        public void ShouldNotTrackChangesIfConfiguredAsNoTracking()
        {
        }
        [Fact]
        public void ShouldNotTrackChangesIfContextInstanceConfiguredAsNoTracking()
        {
        }
    }
}

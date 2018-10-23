using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.D_ScalarFunctionMapping
{
    public class ScalarFunctionTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ScalarFunctionTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        //TODO: Add Scalar Function Mapping to DbContext
        [Fact]
        public void ShouldUseFunctionInLinq()
        {
        }

    }
}

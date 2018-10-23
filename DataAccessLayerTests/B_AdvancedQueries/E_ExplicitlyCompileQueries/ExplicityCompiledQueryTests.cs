using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.E_ExplicitlyCompileQueries
{
    public class ExplicityCompiledQueryTests : IDisposable
    {
        //TODO: add precompiled query

        private readonly AdventureWorksContext _context;

        public ExplicityCompiledQueryTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        //TODO: Add Compiled Query
        [Fact]
        public void ShouldUsePreCompiledQuery()
        {
        }
    }
}

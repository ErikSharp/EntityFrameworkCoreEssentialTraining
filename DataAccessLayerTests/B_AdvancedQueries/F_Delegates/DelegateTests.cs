using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.F_Delegates
{
    public class DelegateTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public DelegateTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //TODO: Add New Class and Extension Methods
        [Fact]
        public void ShouldReturnTableAsQueryableWhenNoWhereClauses()
        {
        }
        [Fact]
        public void ShouldAddSingleWhereClauses()
        {
        }
        [Fact]
        public void ShouldAddMultipleWhereClauses()
        {
        }

        [Fact]
        public void ShouldAddIncludes()
        {
        }

        [Fact]
        public void ShouldAddNestedIncludes()
        {
        }

        [Fact]
        public void ShouldAddOrderByClauses()
        {
        }

    }
}

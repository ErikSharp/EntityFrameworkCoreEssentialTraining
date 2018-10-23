using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.A_RawSqlWithLinq
{
    public class RawSqlWithLinqTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public RawSqlWithLinqTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        //TODO: Add DbContextExtensions to Extensions folder
        [Fact]
        public void ShouldGetSqlServerSchemaAndTableName()
        {
        }

        [Fact]
        public void ShouldGetAllProductsWithInlineSql()
        {
        }
        [Fact]
        public void ShouldGetAllProductsWithInlineSqlIgnoringQueryFilter()
        {
        }
        [Fact]
        public void ShouldGetAllProductsWithInlineSqlAndUseLinqToGetRelatedData()
        {
        }
        //TODO: Add Stored Procedure
        [Fact]
        public void ShouldGetAllProductsWithStoredProcedure()
        {
        }
        [Fact]
        public void ShouldFailWithStoredProcedureAndInclude()
        {
        }
        [Fact]
        public void ShouldRunClientSideWithStoredProcedureAndOrderBy()
        {
        }
        //TODO: Add ProductViewModel to DbContext as notmapped
        [Fact]
        public void ShouldPopulateViewModelWithInlineSql()
        {
        }
    }
}

using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.B_GeneratedSQLAndLogging
{
    public class GeneratedSqlTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public GeneratedSqlTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldGetSqlWithSimpleQuery()
        {
        }

        //TODO: Add Logging
        [Fact]
        public void GetGeneratedSqlFromLinqStatement()
        {
        }

    }
}
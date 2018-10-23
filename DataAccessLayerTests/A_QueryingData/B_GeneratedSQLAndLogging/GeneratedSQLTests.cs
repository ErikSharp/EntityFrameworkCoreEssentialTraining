using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
            IQueryable<Product> query = _context.Product;
            string generatedSql = query.ToSql();
            Debugger.Break();
        }

        [Fact]
        public void GetGeneratedSqlFromLinqStatement()
        {
            var query = _context.Product
                .Where(x => x.MakeFlag ?? true)
                .OrderBy(x => x.Name)
                .Skip(25)
                .Take(25);

            var results = query.ToList();
            Debugger.Break();
        }

    }
}
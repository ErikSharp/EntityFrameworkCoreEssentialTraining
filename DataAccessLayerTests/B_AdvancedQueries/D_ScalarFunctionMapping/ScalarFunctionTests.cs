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

        [Fact]
        public void ShouldUseDbFunctionsInLinq()
        {
            //Notice the static method GetStock AdventureWorksContext.partial
            //that makes this work by mapping the function in the database

            //This makes the query use a database function so nothing is evaluated client-side

            var prodList = _context.Product
                .Where(p => AdventureWorksContext.GetStock(p.ProductId) > 4).ToList();

            Assert.Equal(191, prodList.Count);
        }

    }
}

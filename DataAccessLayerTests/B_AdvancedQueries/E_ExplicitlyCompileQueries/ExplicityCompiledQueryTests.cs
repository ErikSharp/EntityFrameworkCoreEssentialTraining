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
        public static Func<AdventureWorksContext, decimal, bool, IEnumerable<Product>>
            GetProductByListPriceAndMakeFlag =
                EF.CompileQuery((AdventureWorksContext context, decimal listPrice, bool makeFlag) =>
                    context.Product.Where(p => p.ListPrice >= listPrice && (p.MakeFlag ?? false) == makeFlag)
                );
            
        private readonly AdventureWorksContext _context;

        public ExplicityCompiledQueryTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldUsePreCompiledQuery()
        {
            //This might be useful in high-volume / high-load scenarios
            var products = 
                GetProductByListPriceAndMakeFlag(_context, 0m, true)
                .ToList();

            Assert.Equal(163, products.Count);
        }
    }
}

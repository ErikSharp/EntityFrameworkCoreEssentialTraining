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

        [Fact]
        public void ShouldReturnTableAsQueryableWhenNoWhereClauses()
        {
            IQueryable<Product> query = _context.Product.AddWheres();
            Assert.Equal(406, query.ToList().Count);
        }

        [Fact]
        public void ShouldAddSingleWhereClauses()
        {
            var query = _context.Product.AddWheres(p => p.ListPrice > 0);
            Assert.Equal(206, query.ToList().Count);
        }

        [Fact]
        public void ShouldAddMultipleWhereClauses()
        {
            var query = _context.Product.AddWheres(p => p.ListPrice > 0, p => p.MakeFlag ?? true);
            Assert.Equal(145, query.ToList().Count);
        }

        [Fact]
        public void ShouldAddIncludes()
        {
            var product = _context.Product
                .AddWheres(p => p.ProductModelId != null)
                .AddIncludes(p => p.Include(x => x.ProductModel)).First();

            Assert.NotNull(product.ProductModel);
        }

        [Fact]
        public void ShouldAddNestedIncludes()
        {
            IQueryable<Product> query = _context.Product.AddWheres(p => p.ProductModelId == 7);
            query = query.AddIncludes(
                p => p.Include(x => x.ProductModel).ThenInclude(x => x.ProductModelIllustration),
                p => p.Include(x => x.ProductInventory).ThenInclude(x => x.Location));

            var prod = query.First();

            Assert.NotNull(prod.ProductModel);
            Assert.NotNull(prod.ProductModel.ProductModelIllustration);
            Assert.NotEmpty(prod.ProductModel.ProductModelIllustration);
        }

        [Fact]
        public void ShouldAddOrderByClauses()
        {
            var query = _context.Product
                .AddWheres(p => p.ListPrice != 0)
                .AddOrderBys((p => p.ListPrice, true), (p => p.Name, true));

            var prodList = query.ToList();
            for (int x = 1; x < prodList.Count; x++)
            {
                Assert.True(prodList[x].ListPrice >= prodList[x - 1].ListPrice);
                if (prodList[x].ListPrice == prodList[x - 1].ListPrice)
                {
                    Assert.True(String.Compare(prodList[x - 1].Name, prodList[x - 1].Name, StringComparison.OrdinalIgnoreCase) == 0);
                }
            }
        }

    }
}

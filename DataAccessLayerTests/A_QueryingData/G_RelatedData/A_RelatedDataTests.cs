using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.G_RelatedData
{
    public class EagerLoadRelatedDataTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public EagerLoadRelatedDataTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldNotLoadRelatedDataByDefault()
        {
            List<Product> products = _context.Product
                .Where(p => p.ProductModelId == 7).ToList();

            Assert.Null(products[0].ProductModel);
        }

        [Fact]
        public void ShouldEagerlyLoadRelatedData()
        {
            List<Product> products = _context.Product
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                .ToList();

            Assert.NotNull(products[0].ProductModel);
        }

        [Fact]
        public void ShouldEagerlyLoadMultipleLevelsRelatedData()
        {
            List<Product> products = _context.Product
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                    .ThenInclude(m => m.ProductModelIllustration)
                    .ThenInclude(i => i.Illustration)
                .Include(p => p.ProductCostHistory)
                .ToList();

            Assert.NotNull(products[0].ProductModel);
            Assert.NotNull(products[0].ProductModel.ProductModelIllustration);
            Assert.NotNull(products[0].ProductModel.ProductModelIllustration.First().Illustration);
            Assert.NotEmpty(products[0].ProductCostHistory);
        }

        [Fact]
        public void ShouldIgnoreIncludeWithProjections()
        {
            var query = _context.Product
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                    .ThenInclude(m => m.ProductModelIllustration)
                    .ThenInclude(i => i.Illustration)
                .Include(p => p.ProductCostHistory);

            var products = query.Select(p => new
            {
                p.ProductId,
                p.Name,
                p.ListPrice
            }).ToList();

            //You will notice that the sql statement will not have any joins
        }

        [Fact]
        public void ShouldReattachRelatedEntries()
        {
            Product prod = _context.Product.FirstOrDefault(p => p.ProductModelId == 7);
            Assert.Null(prod.ProductModel);
            ProductModel productModel = _context.ProductModel.FirstOrDefault(m => m.ProductModelId == 7);
            Assert.NotNull(productModel);
        }
    }
}

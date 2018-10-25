using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.G_RelatedData
{
    public class ExplicitlyLoadRelatedDataTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ExplicitlyLoadRelatedDataTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldEagerlyLoadRelatedData()
        {
            Product product = _context.Product.FirstOrDefault(p => p.ProductModelId == 7);
            Assert.Null(product.ProductModel);

            //you will notice that this second call calls a stored procedure
            //to get the data - it is also parameterized      
            _context.Entry(product).Reference(p => p.ProductModel).Load();
            Assert.NotNull(product.ProductModel);
        }

        [Fact]
        public void ShouldEagerlyLoadMultipleLevelsRelatedData()
        {
            //this results in three queries to the database
            Product product = _context.Product.FirstOrDefault(p => p.ProductModelId == 7);    
            _context.Entry(product).Reference(p => p.ProductModel).Load();
            _context.Entry(product.ProductModel).Collection(pm => pm.ProductModelIllustration).Load();

            Assert.Equal(1, product.ProductModel.ProductModelIllustration.Count);
        }

        [Fact]
        public void ShouldQueryRelatedData()
        {
            ProductModel pm = _context.ProductModel.Find(7);
            int count = _context.Entry(pm)
                .Collection(p => p.Product).Query().Count(p => p.Weight > 3);

            Assert.Equal(4, count);
        }

        [Fact]
        public void ShouldAddAllRelatedData()
        {
            ProductModel pm = _context.ProductModel.Find(7);
            int count = _context.Entry(pm)
                .Collection(p => p.Product).Query().Count();

            Assert.Equal(8, count);
        }

    }
}
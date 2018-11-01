using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.F_DetachedEntities
{
    public class AttachingEntitiesTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public AttachingEntitiesTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WhenAttachingWithNullOrDefaultPrimaryKeys()
        {
            var productAdded = TestHelpers.CreateProduct("1");
            _context.Attach(productAdded);
            Assert.Equal(EntityState.Added, _context.Entry(productAdded).State);
            _context.Entry(productAdded).State = EntityState.Detached;

            //can attach either on the context or the DbSet
            _context.Product.Attach(productAdded);
            Assert.Equal(EntityState.Added, _context.Entry(productAdded).State);
            _context.Entry(productAdded).State = EntityState.Detached;

            _context.Product.Update(productAdded);
            Assert.Equal(EntityState.Added, _context.Entry(productAdded).State);
            _context.Entry(productAdded).State = EntityState.Detached;

            Assert.Throws<InvalidOperationException>(() => 
                _context.Product.Remove(productAdded));
        }

        [Fact]
        public void WhenAttachingWithPrimaryKeyValues()
        {
            var product = _context.Product.AsNoTracking().First();
            product.Name += " Test";
            Assert.Empty(_context.ChangeTracker.Entries());

            _context.Attach(product);
            Assert.Equal(EntityState.Unchanged, _context.Entry(product).State);
            _context.Entry(product).State = EntityState.Detached;

            _context.Product.Attach(product);
            Assert.Equal(EntityState.Unchanged, _context.Entry(product).State);
            _context.Entry(product).State = EntityState.Detached;

            _context.Product.Update(product);
            Assert.Equal(EntityState.Modified, _context.Entry(product).State);
            _context.Entry(product).State = EntityState.Detached;

            _context.Product.Add(product);
            Assert.Equal(EntityState.Added, _context.Entry(product).State);
            _context.Entry(product).State = EntityState.Detached;
        }

        [Fact]
        public void ShouldDeleteEntityUsingState()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                //Arrange the test
                var product = TestHelpers.CreateProduct("1");
                _context.Add(product);
                _context.SaveChanges();
                var productId = product.ProductId;
                Assert.Equal(1, _context.Product.Count(p => p.ProductId == productId));

                _context.Entry(product).State = EntityState.Detached;

                //Act
                _context.Entry(product).State = EntityState.Deleted;
                _context.SaveChanges();

                //Assert
                Assert.Equal(0, _context.Product.Count(p => p.ProductId == productId));

                transaction.Rollback();
            }
        }

        [Fact]
        public void ShouldDeleteEntityUsingStateWhenTracked()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var productAdded = TestHelpers.CreateProduct("1");
                _context.Product.Add(productAdded);
                _context.SaveChanges();
                //_context.Entry(productAdded).State = EntityState.Detached;
                var pkId = productAdded.ProductId;
                Assert.Equal(1, _context.Product.Count(p => p.ProductId == pkId));
                DeleteProductUsingState(pkId, productAdded.Name);
                Assert.Equal(0, _context.Product.Count(p => p.ProductId == pkId));
                transaction.Rollback();
            }
        }

        internal void DeleteProductUsingState(int productId, string name)
        {
            var prod = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(p => p.Entity.ProductId == productId);
            
            if (prod != null)
            {
                prod.State = EntityState.Deleted;
            }
            else
            {
                var prodToDelete = new Product
                {
                    ProductId = productId,
                    Name = name
                };

                _context.Remove(prodToDelete);
            }

            _context.SaveChanges();
        }
    }
}
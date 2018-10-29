using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.A_BasicSave
{
    public class BasicSaveTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public BasicSaveTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldNotTrackUntrackedRecords()
        {
            var product = TestHelpers.CreateProduct("1");
            Assert.Equal(EntityState.Detached, _context.Entry(product).State);
            Assert.Empty(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void ShouldTrackUnchangedRecords()
        {
            var productUnchanged = _context.Product.First();
            Assert.Equal(EntityState.Unchanged, _context.Entry(productUnchanged).State);
            Assert.Single(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void ShouldTrackAddedRecords()
        {
            var product1 = TestHelpers.CreateProduct("1");
            _context.Product.Add(product1);
            Assert.Equal(EntityState.Added, _context.Entry(product1).State);
            Assert.Single(_context.ChangeTracker.Entries());

            //this shows that you can add directly to the context if you want
            var product2 = TestHelpers.CreateProduct("2");
            _context.Add(product2);
            Assert.Equal(EntityState.Added, _context.Entry(product1).State);
            Assert.Equal(2, _context.ChangeTracker.Entries().Count());
        }

        [Fact]
        public void ShouldTrackRemovedRecords()
        {
            var productToRemove = _context.Product.OrderBy(p => p.ProductId).First();
            _context.Product.Remove(productToRemove);
            Assert.Equal(EntityState.Deleted, _context.Entry(productToRemove).State);

            //again you can remove straight from the context
            var productToRemove2 = _context.Product.OrderBy(p => p.ProductId).Skip(1).First();
            _context.Remove(productToRemove2);
            Assert.Equal(EntityState.Deleted, _context.Entry(productToRemove2).State);

            var productToRemove3 = TestHelpers.CreateProduct("3");
            productToRemove3.ProductId = 3;
            _context.Product.Remove(productToRemove3);
            Assert.Equal(EntityState.Deleted, _context.Entry(productToRemove3).State);
        }

        [Fact]
        public void ShouldTrackChangedEntities()
        {
            var product1 = _context.Product.OrderBy(p => p.ProductId).First();
            product1.Name += " test";
            Assert.Equal(EntityState.Modified, _context.Entry(product1).State);

            var product2 = TestHelpers.CreateProduct("2");
            product2.ProductId = 2;
            _context.Product.Update(product2);
            Assert.Equal(EntityState.Modified, _context.Entry(product2).State);

            //call directly on the context
            var product3 = TestHelpers.CreateProduct("3");
            product3.ProductId = 3;
            _context.Update(product3);
            Assert.Equal(EntityState.Modified, _context.Entry(product3).State);
        }

        [Fact]
        public void ShouldResetStateOfEntitiesAfterSaveChanges()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product1 = _context.Product.OrderBy(p => p.ProductId).First();
                product1.Name += " test";
                Assert.Equal(EntityState.Modified, _context.Entry(product1).State);
                _context.SaveChanges();
                Assert.Equal(EntityState.Unchanged, _context.Entry(product1).State);

                transaction.Rollback();
            }
        }

        [Fact]
        public void ShouldManuallyResetStateOfEntitiesAfterSaveChanges()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product1 = _context.Product.OrderBy(p => p.ProductId).First();
                product1.Name += " test";
                Assert.Equal(EntityState.Modified, _context.Entry(product1).State);

                //Here's how you change the EntityState manually - not sure why you would want to do this
                _context.SaveChanges(false);
                Assert.Equal(EntityState.Modified, _context.Entry(product1).State);
                _context.ChangeTracker.AcceptAllChanges();
                Assert.Equal(EntityState.Unchanged, _context.Entry(product1).State);

                transaction.Rollback();
            }
        }
    }
}
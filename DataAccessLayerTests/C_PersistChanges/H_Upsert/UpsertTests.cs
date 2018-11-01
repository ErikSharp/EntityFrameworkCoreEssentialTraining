using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.H_Upsert
{
    public class UpsertTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public UpsertTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldDoAnInsertOfNewRecordWithUpsert()
        {
            var product = TestHelpers.CreateProduct("1");
            _context.Product.AddOrUpdate(product, p => p.Name);
            var newEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(p => p.Entity.ProductId == product.ProductId);

            Assert.NotNull(newEntry);
            Assert.Equal(EntityState.Added, newEntry.State);
        }

        [Fact]
        public void ShouldDoAnUpdateOfExistingRecordWithUpsert()
        {
            var product = TestHelpers.CreateProduct("1");
            product.Name = "BB Ball Bearing";
            _context.Product.AddOrUpdate(product, x => x.Name);
            var updatedEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(x => x.Entity.Name == product.Name);

            Assert.NotNull(updatedEntry);
            Assert.Equal(EntityState.Modified, updatedEntry.State);
        }

        [Fact]
        public void ShouldDoAnUpdateOfExistingRecordWithUpsertUsingMultipleKeys()
        {
            var product = TestHelpers.CreateProduct("1");
            product.Name = "BB Ball Bearing";
            product.ProductNumber = "BE-2349";
            _context.Product.AddOrUpdate(product, x => x.Name, x => x.ProductNumber);
            var updateEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(x => x.Entity.Name == product.Name);

            Assert.NotNull(updateEntry);
            Assert.Equal(EntityState.Modified, updateEntry.State);
        }

        [Fact]
        public void ShouldDoAnUpdateOfExistingRecordWithUpsertUsingMultipleProducts()
        {
            var product1 = TestHelpers.CreateProduct("1");
            product1.Name = "BB Ball Bearing";
            var product2 = TestHelpers.CreateProduct("1");
            product2.Name = "Blade";
            _context.Product.AddOrUpdate(new List<Product> { product1, product2 }, x => x.Name);
            var updatedEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(x => x.Entity.Name == product1.Name);

            Assert.NotNull(updatedEntry);
            Assert.Equal(EntityState.Modified, updatedEntry.State);
            updatedEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(x => x.Entity.Name == product2.Name);
            Assert.NotNull(updatedEntry);
            Assert.Equal(EntityState.Modified, updatedEntry.State);
        }
    }
}

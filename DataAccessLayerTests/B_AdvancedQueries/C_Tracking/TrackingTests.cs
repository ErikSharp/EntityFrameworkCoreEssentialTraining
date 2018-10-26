using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.C_Tracking
{
    public class TrackingTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public TrackingTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldTrackChangesByDefault()
        {
            Product prod = _context.Product.First();
            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();

            Assert.Single(entries);
            var originalName = prod.Name;
            prod.Name = "foo";

            Assert.Equal(
                originalName, 
                entries[0].OriginalValues[nameof(Product.Name)].ToString());

            Assert.Equal(
                prod.Name,
                entries[0].CurrentValues[nameof(Product.Name)].ToString());

            Assert.Equal(EntityState.Modified, _context.Entry(prod).State);
        }

        [Fact]
        public void ShouldNotTrackChangesIfConfiguredAsNoTracking()
        {
            Product prod = _context.Product.AsNoTracking().First();
            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();

            Assert.Empty(entries);
        }

        [Fact]
        public void ShouldNotTrackChangesIfContextInstanceConfiguredAsNoTracking()
        {
            //If you are building a web server, this is the best way to stop tracking
            //as web servers do not need tracking normally

            //You would normally do this in the Context.OnConfiguring with the UseQueryTrackingBehavior() 
            //on the options builder
            _context.ChangeTracker.QueryTrackingBehavior = 
                QueryTrackingBehavior.NoTracking;

            Product prod = _context.Product.First();
            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();

            Assert.Empty(entries);
        }
    }
}

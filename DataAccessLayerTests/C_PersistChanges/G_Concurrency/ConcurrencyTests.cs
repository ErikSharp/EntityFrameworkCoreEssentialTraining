using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.G_Concurrency
{
    public class ConcurrencyTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ConcurrencyTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldThrowErrorWithConcurrencyViolation()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product = TestHelpers.CreateProduct("1");
                _context.Product.Add(product);
                _context.SaveChanges();

                string sql = $"UPDATE {_context.GetSqlServerTableName<Product>()} " +
                    $"SET Name = 'Foo' WHERE {nameof(Product.ProductId)} = {product.ProductId}";

                _context.Database.ExecuteSqlCommand(sql);

                product.MakeFlag = false;
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //Get Current value for name
                    var entityInError = ex.Entries[0];
                    PropertyValues originalValues = entityInError.OriginalValues;
                    PropertyValues proposedValues = entityInError.CurrentValues;
                    //This makes a database call to get the current values in the database
                    PropertyValues databaseValues = entityInError.GetDatabaseValues();

                    Debug.WriteLine("Name");
                    Debug.WriteLine($"Original Name: {originalValues.GetValue<string>(nameof(Product.Name))}");
                    Debug.WriteLine($"Proposed Name: {proposedValues.GetValue<string>(nameof(Product.Name))}");
                    Debug.WriteLine($"Database Name: {databaseValues.GetValue<string>(nameof(Product.Name))}");

                    Debug.WriteLine("Make Flag");
                    //this is just a different syntax to above
                    Debug.WriteLine(
                        $"Original Make Flag: {entityInError.Property(nameof(Product.MakeFlag)).OriginalValue}");
                    Debug.WriteLine(
                        $"Proposed Make Flag: {entityInError.Property(nameof(Product.MakeFlag)).CurrentValue}");

                    var modifiedProperties = entityInError.Properties.Where(p => p.IsModified).ToList();
                    foreach (var p in modifiedProperties)
                    {
                        Debug.WriteLine($"{p.Metadata.Name}:{p.OriginalValue}:{p.CurrentValue}");
                    }

                    //This reloads the entity from the database, discarding any proposed changes
                    //entityInError.Reload();
                }

                transaction.Rollback();
            }
        }
    }
}
using System;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.D_ServerGenerated
{
    public class ServerGeneratedTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ServerGeneratedTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldSetDefaultValuesForKeysWhenCreatedAndUniqueWhenTracked()
        {
            var prod1 = TestHelpers.CreateProduct("1");
            var prod2 = TestHelpers.CreateProduct("2");
            var prod3 = TestHelpers.CreateProduct("3");
            Assert.Equal(0, prod1.ProductId);
            Assert.Equal(0, prod2.ProductId);
            Assert.Equal(0, prod3.ProductId);

            _context.Product.Add(prod1);
            _context.Product.Add(prod2);
            _context.Product.Add(prod3);
            Assert.True(prod1.ProductId < 0);
            Assert.True(prod2.ProductId < 0);
            Assert.True(prod3.ProductId < 0);
            Assert.NotEqual(prod1.ProductId, prod2.ProductId);
            Assert.NotEqual(prod2.ProductId, prod3.ProductId);
            Assert.NotEqual(prod1.ProductId, prod3.ProductId);
        }

        [Fact]
        public void ShouldSetDefaultValuesWhenAddingRecordsAndUpdateFromDatabaseAfterAdd()
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var product = TestHelpers.CreateProduct("1");
                _context.Product.Add(product);
                Assert.True(product.ProductId < 0);
                Assert.Equal(Guid.Empty, product.Rowguid);
                _context.SaveChanges();
                Assert.True(product.ProductId > 0);
                Assert.NotEqual(Guid.Empty, product.Rowguid);
                trans.Rollback();
            }
        }

        [Fact]
        public void ShouldAllowSettingServerGeneratedProperties()
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var product = TestHelpers.CreateProduct("1");
                var g = Guid.NewGuid();
                product.Rowguid = g;
                _context.Product.Add(product);
                Assert.Equal(g, product.Rowguid);
                
                _context.SaveChanges();
                Assert.Equal(g, product.Rowguid);
                
                trans.Rollback();
            }
        }

        [Fact]
        public void ShouldAllowSettingServerGeneratedPropertiesWithIdentityInsert()
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var product = TestHelpers.CreateProduct("1");
                var g = Guid.NewGuid();
                product.Rowguid = g;
                product.ProductId = 99999;
                _context.Product.Add(product);
                Assert.Equal(g, product.Rowguid);
                var sql = $"SET IDENTITY_INSERT " +
                    $"{_context.GetSqlServerTableName<Product>()} ON";

                _context.Database.ExecuteSqlCommand(sql);
                _context.SaveChanges();

                sql = $"SET IDENTITY_INSERT " +
                    $"{_context.GetSqlServerTableName<Product>()} OFF";

                _context.Database.ExecuteSqlCommand(sql);
                
                Assert.Equal(g, product.Rowguid);
                Assert.Equal(99999, product.ProductId);

                trans.Rollback();
            }
        }
    }
}
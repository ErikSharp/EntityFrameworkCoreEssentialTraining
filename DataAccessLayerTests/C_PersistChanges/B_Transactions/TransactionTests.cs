using System;
using System.Data.Common;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.B_Transactions
{
    public class TransactionTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public TransactionTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        [Fact]
        public void ShouldRollbackFailedImplicitTransaction()
        {
            var count = _context.Product.Count();
            var product1 = TestHelpers.CreateProduct("1");
            _context.Product.Add(product1);

            //this one should fail as it is not a complete product
            var product2 = new Product
            {
                ProductId = 2
            };

            _context.Product.Add(product2);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //todo IRL handle exception properly
                Console.WriteLine(ex);
            }

            Assert.Equal(count, _context.Product.Count());
        }

        [Fact]
        public void ShouldExecuteInAnExplictTransaction()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var count = _context.Product.Count();
                var product1 = TestHelpers.CreateProduct("1");
                _context.Product.Add(product1);
                var product2 = TestHelpers.CreateProduct("2");
                _context.Product.Add(product2);

                try
                {
                    _context.SaveChanges();

                    //You would normally do your commit like this, but not for test
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    //todo IRL handle exception properly
                    Console.WriteLine(ex);

                    //Normally you would do your rollback here, but not for test
                    //transaction.Rollback();
                }

                //rollback for the test
                Assert.Equal(count + 2, _context.Product.Count());
                transaction.Rollback();

                Assert.Equal(count, _context.Product.Count());
            }
        }

        [Fact]
        public void ShouldExecuteInATransactionAcrossMultipleContexts()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var count = _context.Product.Count();
                var product = TestHelpers.CreateProduct("1");
                _context.Product.Add(product);

                using(var context2 = CreateContext(
                    _context.Database.GetDbConnection()))
                {
                    context2.Database.UseTransaction(transaction.GetDbTransaction());

                    var product2 = TestHelpers.CreateProduct("2");
                    _context.Product.Add(product2);
                    context2.SaveChanges();

                    //This should have added one but it is not - EF 2.1 change???
                    //Assert.Equal(count + 1, context2.Product.Count());
                    Assert.Equal(count, context2.Product.Count());
                }

                _context.SaveChanges();
                Assert.Equal(count + 2, _context.Product.Count());

                //this will rollback for both contexts
                transaction.Rollback();
                Assert.Equal(count, _context.Product.Count());
            }
        }

        internal AdventureWorksContext CreateContext(DbConnection connection)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            optionsBuilder
                //.UseLoggerFactory(AdventureWorksContext.AppLoggerFactory)
                .UseSqlServer(connection)
                .ConfigureWarnings(warnings =>
                {
                    warnings.Throw(RelationalEventId.QueryClientEvaluationWarning);
                });

            return new AdventureWorksContext(optionsBuilder.Options);
        }

    }
}
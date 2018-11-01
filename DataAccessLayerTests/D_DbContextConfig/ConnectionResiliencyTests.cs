using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayerTests.C_PersistChanges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace DataAccessLayerTests.D_DbContextConfig
{
    public class ConnectionResiliencyTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ConnectionResiliencyTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact(Skip = "This test takes about a minute to run")]
        public void ShouldRetryThenFail()
        {
            //Bad connection string:
            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Advent8ureWorks;Integrated Security=True";

            var context = CreateContext(connectionString, useDefaultRetry: true);
            Assert.Throws<RetryLimitExceededException>(() => context.Product.First());

            var context2 = CreateContext(connectionString, useDefaultRetry: false, useCustomRetry: true);
            Assert.Throws<RetryLimitExceededException>(() => context2.Product.First());
        }

        [Fact]
        public void UseExplicitTransactionWithExecutionStrategy()
        {
            var context = CreateContext(useDefaultRetry: true);
            IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var count = context.Product.Count();
                    var product1 = TestHelpers.CreateProduct("1");
                    context.Product.Add(product1);
                    var product2 = TestHelpers.CreateProduct("2");
                    context.Product.Add(product2);

                    context.SaveChanges();
                    transaction.Rollback();
                }
            });
        }

        [Fact(Skip = "For demonstration purposes only")]
        public void ShouldWrapUpdateInExecutionStrategyTransaction()
        {
            var context = CreateContext(useDefaultRetry: true);
            var strategy = context.Database.CreateExecutionStrategy();

            var productToAdd = new Product { Name = "Test Product" };
            context.Product.Add(productToAdd);

            strategy.ExecuteInTransaction(context,
                operation: c => { c.SaveChanges(acceptAllChangesOnSuccess: false); },
                verifySucceeded: c => c.Product.AsNoTracking().Any(p => p.ProductId == productToAdd.ProductId));

            context.ChangeTracker.AcceptAllChanges();
        }

        internal DbContextOptionsBuilder<AdventureWorksContext> BuildBaseOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            optionsBuilder
                //.UseLoggerFactory(AdventureWorksContext.AppLoggerFactory)
                .ConfigureWarnings(warnings => { warnings.Throw(RelationalEventId.QueryClientEvaluationWarning); });
            return optionsBuilder;
        }

        internal AdventureWorksContext CreateContext(
            string connectionString = "", 
            bool useDefaultRetry = false, bool useCustomRetry = false,
            int? maxBatchSize = null, int? commandTimeOut = null)
        {
            var connStr = !String.IsNullOrEmpty(connectionString)
                ? connectionString
                : _context.Database.GetDbConnection().ConnectionString;
            var optionsBuilder = BuildBaseOptions();
            optionsBuilder
                .UseSqlServer(connStr,
                options =>
                {
                    if (maxBatchSize.HasValue)
                    {
                        options.MaxBatchSize(maxBatchSize.Value);
                    }
                    if (commandTimeOut.HasValue)
                    {
                        options.CommandTimeout(commandTimeOut.Value);
                    }
                    if (useDefaultRetry)
                    {
                        //normally this should be just options.EnableRetryOnFailure()
                        //it is set like this to force the exception type in the first test
                        options.EnableRetryOnFailure(1, TimeSpan.Zero, new List<int>{ 4060 });
                    }
                    if (useCustomRetry)
                    {
                        options.ExecutionStrategy(dependencies => new MyExecutionStrategy(dependencies));
                    }
                });
            return new AdventureWorksContext(optionsBuilder.Options);
            //return new AdventureWorksContext();
        }

    }
}
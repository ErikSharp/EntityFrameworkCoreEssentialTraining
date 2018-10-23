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

        [Fact]
        public void ShouldRetryThenFail()
        {
            //Bad connection string:
            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=AdventureWorks;Integrated Security=True";
        }

        [Fact]
        public void UseExplicitTransactionWithExecutionStrategy()
        {
        }

        [Fact(Skip = "For demonstration purposes only")]
        public void ShouldWrapUpdateInExecutionStrategyTransaction()
        {
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
                    }
                    if (useCustomRetry)
                    {
                        
                    }
                });
            //return new AdventureWorksContext(optionsBuilder.Options);
            return new AdventureWorksContext();
        }

    }
}
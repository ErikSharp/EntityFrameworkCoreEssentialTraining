using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.C_Batching
{
    public class BatchingTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public BatchingTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldBatchStatementsWhenSendingToSqlServer()
        {
        }
        [Fact]
        public void ShouldNotBatchStatementsIfBatchSizeIsOne()
        {
        }

        internal AdventureWorksContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            optionsBuilder
                //.UseLoggerFactory(AdventureWorksContext.AppLoggerFactory)
                .UseSqlServer(_context.Database.GetDbConnection().ConnectionString,
                    options =>
                    {
                        //options.MaxBatchSize(1);
                    })
                .ConfigureWarnings(warnings => { warnings.Throw(RelationalEventId.QueryClientEvaluationWarning); });
            //return new AdventureWorksContext(optionsBuilder.Options);
            return new AdventureWorksContext();
        }
    }
}
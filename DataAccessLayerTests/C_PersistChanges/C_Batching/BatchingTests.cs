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
            using (var trans = _context.Database.BeginTransaction())
            {
                var prod1 = TestHelpers.CreateProduct("1");
                _context.Add(prod1);
                var prod2 = TestHelpers.CreateProduct("2");
                _context.Add(prod2);
                var prod3 = TestHelpers.CreateProduct("3");
                _context.Add(prod3);

                //This will automatically create one statement with three inserts
                _context.SaveChanges();
                trans.Rollback();
            }
        }

        [Fact]
        public void ShouldNotBatchStatementsIfBatchSizeIsOne()
        {
            using (var context = CreateContext())
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    var prod1 = TestHelpers.CreateProduct("1");
                    context.Add(prod1);
                    var prod2 = TestHelpers.CreateProduct("2");
                    context.Add(prod2);
                    var prod3 = TestHelpers.CreateProduct("3");
                    context.Add(prod3);

                    //This will create three statements as the context has turned off batching
                    context.SaveChanges();
                    trans.Rollback();
                }
            }
        }

        internal AdventureWorksContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            optionsBuilder
                //.UseLoggerFactory(AdventureWorksContext.AppLoggerFactory)
                .UseSqlServer(_context.Database.GetDbConnection().ConnectionString,
                    options =>
                    {
                        //this will turn off batching
                        options.MaxBatchSize(1);
                    })
                .ConfigureWarnings(warnings => { warnings.Throw(RelationalEventId.QueryClientEvaluationWarning); });
            return new AdventureWorksContext(optionsBuilder.Options);
        }
    }
}
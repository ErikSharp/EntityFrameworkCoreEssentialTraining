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
        }

        [Fact]
        public void ShouldExecuteInAnExplictTransaction()
        {
        }

        [Fact]
        public void ShouldExecuteInATransactionAcrossMultipleContexts()
        {
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
            //TODO: Update adventureworks with constructors
            return new AdventureWorksContext();
            //return new AdventureWorksContext(optionsBuilder.Options);
        }

    }
}
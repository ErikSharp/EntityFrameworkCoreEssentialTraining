using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Extensions;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.A_LinqExecution
{
    public class LinqExecutionTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public LinqExecutionTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void LinqNeverExecutesWithoutIteratingValues()
        {
        }

        [Fact]
        public void LinqExecutesOnToList()
        {
        }
    }
}
using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.B_ClientServerEval
{
    public class ClientServerEvaluationTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ClientServerEvaluationTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        //Add warning as error to onconfiguring
        [Fact]
        public void ShouldExecuteClientSide()
        {
        }

        [Fact]
        public void ShouldExecuteServerSide()
        {
        }

    }
}
using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using Microsoft.EntityFrameworkCore;
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

        
        [Fact]
        public void ShouldThrowWhenWillExecuteClientSide()
        {
            //Add warning as error to Context.OnConfiguring
            //This will cause it to throw whenever EF thinks it is running client side

            var sqlQuery = "execute uspGetAllProducts";
            Assert.Throws<InvalidOperationException>(
                () => _context.Product
                    .FromSql(sqlQuery)
                    .Where(p => p.ProductId == 749)
                    .ToList());
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.D_LikeOperator
{
    public class LikeOperatorTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public LikeOperatorTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldGetProductsWithoutUsingLikeOperator()
        {
            var products = _context.Product
                .Where(p => p.Name.Contains("Crankarm"))
                .ToList();

            Assert.Equal(3, products.Count);
        }

        [Fact]
        public void ShouldGetProductsUsingLikeOperator()
        {
            //EF.Functions are for provider (SQL Server) specific functions 
            //This will actually create a query that uses LIKE
            var products = _context.Product
                .Where(p => EF.Functions.Like(p.Name, "%Crankarm%"))
                .ToList();

            Assert.Equal(3, products.Count);
        }
    }
}

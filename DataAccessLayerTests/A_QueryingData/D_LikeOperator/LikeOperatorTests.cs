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
        }

        [Fact]
        public void ShouldGetProductsUsingLikeOperator()
        {
        }
    }
}

using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.F_DetachedEntities
{
    public class AttachingEntitiesTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public AttachingEntitiesTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WhenAttachingWithNullOrDefaultPrimaryKeys()
        {
        }

        [Fact]
        public void WhenAttachingWithPrimaryKeyValues()
        {
        }

        [Fact]
        public void ShouldDeleteEntityUsingState()
        {
        }
        [Fact]
        public void ShouldDeleteEntityUsingStateWhenTracked()
        {
        }

        internal void DeleteProductUsingState(int productId, string name)
        {
        }
    }
}
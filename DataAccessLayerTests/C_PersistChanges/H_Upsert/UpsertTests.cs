using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.H_Upsert
{
    public class UpsertTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public UpsertTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldDoAnInsertOfNewRecordWithUpsert()
        {
        }
        [Fact]
        public void ShouldDoAnUpdateOfExistingRecordWithUpsert()
        {
        }
        [Fact]
        public void ShouldDoAnUpdateOfExistingRecordWithUpsertUsingMultipleKeys()
        {
        }
        [Fact]
        public void ShouldDoAnUpdateOfExistingRecordWithUpsertUsingMultipleProducts()
        {
        }
    }
}

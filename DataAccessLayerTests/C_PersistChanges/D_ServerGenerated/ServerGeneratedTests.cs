using System;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.D_ServerGenerated
{
    public class ServerGeneratedTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ServerGeneratedTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldSetDefaultValuesForKeysWhenCreatedAndUniqueWhenTracked()
        {
        }

        [Fact]
        public void ShouldSetDefaultValuesWhenAddingRecordsAndUpdateFromDatabaseAfterAdd()
        {
        }

        [Fact]
        public void ShouldAllowSettingServerGeneratedProperties()
        {
        }

        [Fact]
        public void ShouldAllowSettingServerGeneratedPropertiesWithIdentityInsert()
        {
        }
    }
}
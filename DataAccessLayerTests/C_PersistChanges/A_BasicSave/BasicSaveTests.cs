using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.A_BasicSave
{
    public class BasicSaveTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public BasicSaveTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //TODO: Include Test Helpers class in project
        [Fact]
        public void ShouldNotTrackUntrackedRecords()
        {
        }

        [Fact]
        public void ShouldTrackUnchangedRecords()
        {
        }

        [Fact]
        public void ShouldTrackAddedRecords()
        {
        }

        [Fact]
        public void ShouldTrackRemovedRecords()
        {
        }

        [Fact]
        public void ShouldTrackChangedEntities()
        {
        }

        [Fact]
        public void ShouldResetStateOfEntitiesAfterSaveChanges()
        {
        }

        [Fact]
        public void ShouldManuallyResetStateOfEntitiesAfterSaveChanges()
        {
        }
    }
}
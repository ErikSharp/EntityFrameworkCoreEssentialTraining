using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.G_Concurrency
{
    public class ConcurrencyTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ConcurrencyTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        //TODO: Make name a concurrency token of Product
        [Fact]
        public void ShouldThrowErrorWithConcurrencyViolation()
        {
        }
    }
}
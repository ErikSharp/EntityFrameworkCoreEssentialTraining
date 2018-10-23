using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.A_RawSqlWithLinq
{

    public class RawSqlWithParametersTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public RawSqlWithParametersTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //TODO: Add WhereUsedViewModel to DbContext as notmapped
        [Fact]
        public void ShouldGetViewModelWithSimpleOrderedParameters()
        {
        }
        [Fact]
        public void ShouldGetViewModelWithTraditionalNamedParameters()
        {
        }

        [Fact]
        public void ShouldGetViewModelWithStringInterpolation()
        {
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.C_FilterSortAndPage
{
    public class FilterTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public FilterTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        [Fact]
        public void ShouldFindWithPrimaryKey()
        {
        }
        [Fact]
        public void ShouldReturnNullIfPrimaryKeyIsNotFound()
        {
        }
        [Fact]
        public void FilteringResultsWithFindComplexKey()
        {
        }

        [Fact]
        public void FilterWithSimpleWhereClause()
        {
        }
        [Fact]
        public void FilterWithMultipleStatementWhereClauses()
        {
        }

        [Fact]
        public void FilterWithBuildingWhereClauses()
        {
        }

        [Fact]
        public void SHouldBeCarefulWithOrClauses()
        {
        }
        [Fact]
        public void FilterWithListOfIds()
        {
        }

        [Fact]
        public void ShouldGetTheFirstRecord()
        {
        }

        [Fact]
        public void ShouldThrowWhenFirstFails()
        {
        }

        [Fact(Skip="Executes client side")]
        public void ShouldGetTheLastRecord()
        {
        }
        [Fact]
        public void ShouldReturnNullWhenRecordNotFound()
        {
        }

        [Fact]
        public void ShouldReturnJustOneRecordWithSingle()
        {
        }
        [Fact]
        public void ShouldFailIfMoreThanOneRecordWithSingle()
        {
        }

    }

}

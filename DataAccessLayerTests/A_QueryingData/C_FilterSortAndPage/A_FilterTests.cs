using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Microsoft.EntityFrameworkCore;
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
            //Find executes immediately
            Product prod = _context.Product.Find(3);
            Assert.Equal("BB Ball Bearing", prod.Name);

            //This will not execute another query as entity is already tracked
            Product prod2 = _context.Product.Find(3);
            Assert.Same(prod, prod2);
        }

        [Fact]
        public void ShouldReturnNullIfPrimaryKeyIsNotFound()
        {
            Product prod = _context.Product.Find(-1);
            Assert.Null(prod);
        }

        [Fact]
        public void FilteringResultsWithFindComplexKey()
        {
        }

        [Fact]
        public void FilterWithSimpleWhereClause()
        {
            List<Product> prodList = _context.Product.Where(p => 
                (p.MakeFlag ?? true) && p.SellEndDate == null)
            .ToList();

            Assert.Equal(163, prodList.Count);
        }

        [Fact]
        public void FilterWithMultipleStatementWhereClauses()
        {
            List<Product> prodList = _context.Product.IgnoreQueryFilters().Where(p =>
                (p.MakeFlag ?? true) && 
                (p.SellEndDate == null || p.ListPrice < 100M))
            .ToList();

            Assert.Equal(167, prodList.Count);
        }

        [Fact]
        public void FilterWithBuildingWhereClauses()
        {
            IQueryable<Product> query = _context.Product
                .Where(p => p.SellEndDate == null);

            query = query.Where(p => p.MakeFlag ?? true);

            Assert.Equal(163, query.ToList().Count);
        }

        [Fact]
        public void FilterWithListOfIds()
        {
            //this is how an IN statement is done on the server
            List<int> list = new List<int> { 1, 3, 5 };
            var query = _context.Product.Where(p => list.Contains(p.ProductId));
            var results = query.ToList();

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void ShouldGetTheFirstRecord()
        {
            var prod = _context.Product.First(p => p.MakeFlag ?? true);
            Assert.Equal("BB Ball BeARing", prod.Name, ignoreCase: true);
        }

        [Fact]
        public void ShouldThrowWhenFirstFails()
        {
            var ex = Assert.Throws<InvalidOperationException>(
                () => _context.Product.First(p => p.ProductId == -1));

            Assert.Equal("Sequence contains no elements", ex.Message, ignoreCase: true);
        }

        [Fact]
        public void ShouldGetTheLastRecord()
        {
            var prod = _context.Product.OrderBy(p => p.ProductId).LastOrDefault();
            Assert.Equal("Road-750 Black, 52", prod.Name);
        }

        [Fact]
        public void ShouldReturnNullWhenRecordNotFound()
        {
            var prod = _context.Product.FirstOrDefault(p => p.ProductId == -1);
            Assert.Null(prod);
        }

        [Fact]
        public void ShouldReturnJustOneRecordWithSingle()
        {
            var prod = _context.Product.SingleOrDefault(p => p.ProductId == 3);
            Assert.Equal("BB Ball Bearing", prod.Name);
        }
        [Fact]

        public void ShouldFailIfMoreThanOneRecordWithSingle()
        {
            var ex = Assert.Throws<InvalidOperationException>(
                () => _context.Product.SingleOrDefault(p => p.MakeFlag ?? true));

            Assert.Equal("Sequence contains more than one element", ex.Message, ignoreCase: true);
        }
    }
}

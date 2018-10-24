using System;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.C_FilterSortAndPage
{
    public class SortingTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public SortingTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldSortData()
        {
            IOrderedQueryable<Product> query = _context.Product
                .Where(p => p.ListPrice != 0)
                .OrderBy(p => p.ListPrice)
                .ThenByDescending(p => p.DaysToManufacture)
                .ThenBy(p => p.Name);

            var prodList = query.ToList();
            for (int i = 1; i < prodList.Count; i++)
            {
                Assert.True(prodList[i].ListPrice >= prodList[i - 1].ListPrice);
            }
        }
    }
}
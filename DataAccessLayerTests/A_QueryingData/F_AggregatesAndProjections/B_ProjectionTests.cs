using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.ViewModels;
using Xunit;

namespace DataAccessLayerTests.A_QueryingData.F_AggregatesAndProjections
{
    public class ProjectionTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ProjectionTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldCreateNewAnonymousObjects()
        {
            //Keep in mind that projections are not tracked
            var newObjectList = _context.Product
                .Where(p => p.MakeFlag ?? true)
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.ListPrice
                })
                .OrderBy(p => p.Name)
                .ToList();

            Assert.Equal(163, newObjectList.Count);
            Assert.Equal(3, newObjectList[0].ProductId);
        }

        //TODO: Copy in ProductViewModel from Assets folder
        [Fact]
        public void ShouldCreateNewViewModels()
        {
            List<ProductViewModel> newObjectList =
                _context.Product
                    .Where(p => p.MakeFlag ?? true)
                    .Select(p => new ProductViewModel
                    {
                        ProductId = p.ProductId,
                        Name = p.Name,
                        ListPrice = p.ListPrice,
                        Color = p.Color,
                        Size = p.Size,
                        StandardCost = p.StandardCost
                    })
                    .ToList();

            Assert.Equal(163, newObjectList.Count);
            Assert.Equal(3, newObjectList[0].ProductId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.C_PersistChanges.E_RelatedData
{
    public class RelatedDataTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public RelatedDataTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void ShouldAddRelatedData()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var catCount = _context.ProductCategory.Count();
                var subCatCount = _context.ProductSubcategory.Count();
                var productCategory = new ProductCategory
                {
                    Name = "New Category",

                    ProductSubcategory = new List<ProductSubcategory>
                    {
                        new ProductSubcategory
                        {
                            Name = "New SubCategory1"
                        },
                        new ProductSubcategory
                        {
                            Name = "New SubCategory2"
                        },
                        new ProductSubcategory
                        {
                            Name = "New SubCategory3"
                        }
                    }
                };

                //the child records will get added automatically
                _context.ProductCategory.Add(productCategory);
                _context.SaveChanges();

                productCategory.ProductSubcategory.ToList().ForEach(psc =>
                    Assert.Equal(psc.ProductCategoryId, productCategory.ProductCategoryId));

                Assert.Equal(catCount + 1, _context.ProductCategory.Count());
                Assert.Equal(subCatCount + 3, _context.ProductSubcategory.Count());

                //This fails because cascade delete is not enabled
                //_context.ProductCategory.Remove(productCategory);
                //Assert.Throws<InvalidOperationException>(() => _context.SaveChanges());

                //This works however, since all changes are done as a unit
                //...even if they are done in the wrong order
                _context.ProductCategory.Remove(productCategory);
                _context.ProductSubcategory.RemoveRange(productCategory.ProductSubcategory);
                _context.SaveChanges();
                transaction.Rollback();
            }
        }
    }
}
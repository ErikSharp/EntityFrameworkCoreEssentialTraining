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

        [Fact]
        public void ShouldGetViewModelWithSimpleOrderedParameters()
        {
            List<WhereUsedViewModel> vmList =
                _context.WhereUsedViewModel
                    .FromSql("execute dbo.uspGetWhereUsedProductID {0}, {1}",
                    3, new DateTime(2017, 12, 25)).ToList();

            Assert.Equal(100, vmList.Count);
        }

        [Fact]
        public void ShouldGetViewModelWithTraditionalNamedParameters()
        {
            var productId = new SqlParameter("startproductid", 3);
            var checkDate = new SqlParameter("checkdate", new DateTime(2017, 12, 25));

            List<WhereUsedViewModel> vmList =
                _context.WhereUsedViewModel
                    .FromSql("execute dbo.uspGetWhereUsedProductID @StartProductID, @CheckDate", 
                        productId, checkDate).ToList();

            Assert.Equal(100, vmList.Count);
        }

        [Fact]
        public void ShouldGetViewModelWithStringInterpolation()
        {
            //I prefer to do it this way
            List<WhereUsedViewModel> vmList =
                _context.WhereUsedViewModel
                .FromSql($"execute dbo.uspGetWhereUsedProductID {3}, {new DateTime(2017, 12, 25)}").ToList();

            Assert.Equal(100, vmList.Count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.A_RawSqlWithLinq
{
    public class RawSqlWithLinqTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public RawSqlWithLinqTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
        [Fact]
        public void ShouldGetSqlServerSchemaAndTableName()
        {
            var schemaTableName = _context.GetSqlServerTableName<Product>();
            Assert.Equal("Production.Product", schemaTableName);
        }

        [Fact]
        public void ShouldGetAllProductsWithInlineSql()
        {
            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            var sqlQuery = $"Select * from {schemaAndTableName}";
            //this is vulnerable to sql injection attacks - see next test set
            List<Product> prodList = _context.Product.FromSql(sqlQuery).ToList();
            
            //this is still using our global query filter
            Assert.Equal(406, prodList.Count);
        }

        [Fact]
        public void ShouldGetAllProductsWithInlineSqlIgnoringQueryFilter()
        {
            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            var sqlQuery = $"Select * from {schemaAndTableName}";
            //this is vulnerable to sql injection attacks
            List<Product> prodList = _context.Product.IgnoreQueryFilters().FromSql(sqlQuery).ToList();
            
            Assert.Equal(504, prodList.Count);
        }

        [Fact]
        public void ShouldGetAllProductsWithInlineSqlAndUseLinqToGetRelatedData()
        {
            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            var sqlQuery = $"Select * from {schemaAndTableName}";

            //this is vulnerable to sql injection attacks
            List<Product> prodList = _context.Product
                .FromSql(sqlQuery)
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                    .ThenInclude(pm => pm.ProductModelIllustration)
                    .ThenInclude(pmi => pmi.Illustration)
                .Include(p => p.ProductCostHistory)
                .ToList();

            Assert.NotNull(prodList[0].ProductModel
                .ProductModelIllustration.ToList()[0].Illustration);
        }

        [Fact]
        public void ShouldGetAllProductsWithStoredProcedure()
        {
            var sqlQuery = "execute uspGetAllProducts";
            var list = _context.Product
                .IgnoreQueryFilters()
                .FromSql(sqlQuery)
                .ToList();

            Assert.Equal(504, list.Count);
        }

        [Fact]
        public void ShouldFailWithStoredProcedureAndInclude()
        {
            var sqlQuery = "execute uspGetAllProducts";
            var ex = Assert.Throws<InvalidOperationException>(
                () => _context.Product.IgnoreQueryFilters()
                .FromSql(sqlQuery).Include(p => p.ProductModel).ToList());

            Assert.Equal(
                "The Include operation is not supported when calling a stored procedure.", 
                ex.Message, 
                ignoreCase: true);
        }

        [Fact(Skip = "Configured EF to throw when there will be client-side eval")]
        public void ShouldRunClientSideWithStoredProcedureAndOrderBy()
        {
            //var sqlQuery = "execute uspGetAllProducts";
            //var ex = Assert.Throws<InvalidOperationException>(
            //    () => _context.Product.IgnoreQueryFilters()
            //    .FromSql(sqlQuery).OrderBy(p => p.ListPrice).ToList());

            //Assert.Contains(
            //    "The LINQ expression 'orderby [x].ListPrice asc' could not be t",
            //    ex.Message,
            //    StringComparison.OrdinalIgnoreCase);


            //the order by runs on the client
            var sqlQuery = "execute uspGetAllProducts";
            var prodList = _context.Product.IgnoreQueryFilters()
                .FromSql(sqlQuery).OrderByDescending(p => p.ListPrice).ToList();

            Assert.Equal(749, prodList[0].ProductId);
        }

        [Fact]
        public void ShouldPopulateViewModelWithInlineSql()
        {
            //this is useful if you want to return data isn't mapped to a table
            //example is a sp that give stats, so you create the stats POCO as we did below for ProductViewModel
            //ProductViewModel is listed as a DbSet in the partial context file

            var schemaAndTableName = _context.GetSqlServerTableName<Product>();
            var sqlQuery = $"Select ProductId, Name, Color, StandardCost, ListPrice, Size from {schemaAndTableName}";
            List<ProductViewModel> prodList = _context.ProductViewModel.FromSql(sqlQuery).ToList();

            //notice that the global filter isn't used as we are using _context.ProductViewModel
            Assert.Equal(504, prodList.Count);
        }
    }
}

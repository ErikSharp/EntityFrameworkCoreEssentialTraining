using System;
using System.Data.Common;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccessLayerTests.C_PersistChanges
{
    public static class TestHelpers
    {
        public static Product CreateProduct(string itemQualifier)
        {
            return new Product()
            {
                ListPrice = 12M,
                Name = $"Test product {itemQualifier}",
                SellStartDate = new DateTime(2017, 12, 15),
                ProductNumber = $"1234-{itemQualifier}",
                SafetyStockLevel = 500,
                ReorderPoint = 375
            };

        }
    }
}
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.EfStructures.Context
{
    public partial class AdventureWorksContext
    {
        [NotMapped]
        public DbSet<ProductViewModel> ProductViewModel { get; set; }

        [NotMapped]
        public DbSet<WhereUsedViewModel> WhereUsedViewModel { get; set; }

        public static readonly LoggerFactory AppLoggerFactory =
            new LoggerFactory(new[] 
            {
                new ConsoleLoggerProvider((a, b) => true, true)
            });

        internal void AddModelCreatingConfiguration(ModelBuilder modelBuilder)
        {
            //This is a global query filter that will run everytime that query Product
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => p.SellEndDate == null);

            //Even though this is valid code, it will execute client side as EF doesn't understand it
            //modelBuilder.Entity<Product>().HasQueryFilter(p => !p.SellEndDate.HasValue);
            
        }

        //You have to specify the FunctionName in the db if different than GetStock
        [DbFunction(FunctionName = "ufnGetStock", Schema = "dbo")]
        public static int GetStock(int productId)
        {
            //This should never get run
            //This method just tells EF about the database function
            throw new NotImplementedException();
        }
    }
}

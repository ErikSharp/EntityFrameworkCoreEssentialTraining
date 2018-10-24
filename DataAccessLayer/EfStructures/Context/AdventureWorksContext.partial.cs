using DataAccessLayer.EfStructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EfStructures.Context
{
    public partial class AdventureWorksContext
    {
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
    }
}

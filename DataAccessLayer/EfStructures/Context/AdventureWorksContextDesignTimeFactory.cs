using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EfStructures.Context
{
    public class AdventureWorksContextDesignTimeFactory 
        : IDesignTimeDbContextFactory<AdventureWorksContext>
    {
        public AdventureWorksContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();

            var connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AdventureWorks2016;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);

            return new AdventureWorksContext(optionsBuilder.Options);
        }
    }
}

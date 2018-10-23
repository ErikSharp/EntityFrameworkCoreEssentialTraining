using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.ViewModels
{
    public class WhereUsedViewModel
    {
        [Key]
        public int ProductAssemblyId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentDesc { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public short BOMLevel { get; set; }
        public int RecursionLevel { get; set; }

    }

}

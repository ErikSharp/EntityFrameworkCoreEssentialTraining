using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DataAccessLayer.EfStructures.Entities;

namespace DataAccessLayer.ViewModels
{
    public partial class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(4000)]
        public string Name { get; set; }

        public string Color { get; set; }
        [Display(Name="Standard Cost")]
        [Column(TypeName = "money")]
        public decimal StandardCost { get; set; }
        [Display(Name = "List Price")]
        [Column(TypeName = "money")]
        public decimal ListPrice { get; set; }

        [StringLength(5)]
        public string Size { get; set; }
    }
}
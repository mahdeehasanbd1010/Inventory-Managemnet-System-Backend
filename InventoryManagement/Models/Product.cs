using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }
    }
}

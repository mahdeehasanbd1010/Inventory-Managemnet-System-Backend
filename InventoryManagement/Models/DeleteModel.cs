using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class DeleteModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string Token { get; set; }
    }
}

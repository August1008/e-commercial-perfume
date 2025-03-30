using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "Updating";
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }
        [Required]
        public string Type { get; set; } = "";
        [Required]
        public string Brand { get; set; } = "";
        public int Quantity { get; set; }

    }

}

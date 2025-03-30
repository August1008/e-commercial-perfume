using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.RequestModels
{
    public class ProductCreationModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "Updating";
        public string? Description { get; set; } = "Description";
        [Required]
        [Range(0.0,1000000000.0, ErrorMessage ="Price is invalid")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = "Default Url";
        [Required]
        public string Type { get; set; } = "Type";
        [Required]
        public string Brand { get; set; } = "Brand";
        public int Quantity { get; set; }
    }
}

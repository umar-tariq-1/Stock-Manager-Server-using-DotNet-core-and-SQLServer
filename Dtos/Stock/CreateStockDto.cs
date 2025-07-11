using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Stock
{
    public class CreateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol can't be longer than 10 characters.")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Company name can't be longer than 100 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Purchase must be a non-negative value, less than 1000000000")]
        public decimal Purchase { get; set; }

        [Range(0.001, 100, ErrorMessage = "LastDiv must be a non-negative value, between 0.001 and 100")]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Industry can't be longer than 50 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Range(0, long.MaxValue, ErrorMessage = "MarketCap must be a non-negative value less than 5000000000")]
        public long MarketCap { get; set; }
    }
}
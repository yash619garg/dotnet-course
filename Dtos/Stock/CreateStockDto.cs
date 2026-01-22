using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol Can't be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(20, ErrorMessage = "Company Name Can't be over 20 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Industry Name Can't be over 20 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 500000000000)]
        public long MarketCap { get; set; }
    }
}

// this is request dto
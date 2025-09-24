using System.ComponentModel.DataAnnotations;

namespace SaleProducts.Models
{
    public class Printtest
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }
        public decimal SellingPrice { get; set; }

        public int Packages { get; set; }

        public string? Barcode { get; set; }
    }
}

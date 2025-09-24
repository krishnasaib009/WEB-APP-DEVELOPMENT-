using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SaleProducts.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Precision(18, 3)]
        public decimal SellingPrice { get; set; }

        public int Packages { get; set; }

        public string Barcode { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SaleProducts.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        public string? ProductName { get; set; }
        public string? ContainerName { get; set; }
        public  int Qunatity { get; set; }
        public int PurchasedStock { get; set; }
        public string? Barcode { get; set; }
    }
}

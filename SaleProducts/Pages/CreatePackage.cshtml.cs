using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleProducts.Data;
using SaleProducts.Models;

namespace SaleProducts.Pages
{
    public class PackagesModel : PageModel
    {
        private readonly AppDbContext _prdcontext;

        public PackagesModel(AppDbContext prdcontext)
        {
            _prdcontext = prdcontext;
        }
        [BindProperty]
        public Package package { get; set; }
        
        [BindProperty]
        public bool IsEditMode { get; set; }

        public List<Package> PackageList { get; set; } = new List<Package>();
        public void OnGet()
        {
            PackageList = _prdcontext.Packages.ToList();
            package = new Package(); // Optional if using empty form
        }
        public IActionResult OnPostAddProduct()
        {
            if (!ModelState.IsValid)
            {
                PackageList = _prdcontext.Packages.ToList();
                return BadRequest("Invalid package data");
            }

            _prdcontext.Packages.Add(package);
            _prdcontext.SaveChanges();
            return new JsonResult( new
                { 
                success = true, data = new
                { 
                  package.PackageId, 
                  package.ProductName, 
                  package.ContainerName, 
                  package.Barcode 
                }
            });
            return RedirectToPage();
        }
        public IActionResult OnPostEdit(int id)
        {
           var existingProducts = _prdcontext.Packages.FirstOrDefault(p => p.PackageId ==package.PackageId);
            if (existingProducts != null)
            { 
            existingProducts.ProductName = package.ProductName;
            existingProducts.ContainerName = package.ContainerName;
            existingProducts.Qunatity = package.Qunatity;
            existingProducts.PurchasedStock = package.PurchasedStock;
            existingProducts.Barcode = package.Barcode;
            _prdcontext.SaveChanges();
            }
            return new JsonResult(new
            {
                success = true,
                data = existingProducts
            });
        }

        public IActionResult OnPostDelete(int id)
        {
            var packageData = _prdcontext.Packages.FirstOrDefault(p => p.PackageId == id);
            if (packageData == null)
            {
                return NotFound();
               
            }
            _prdcontext.Packages.Remove(packageData);
            _prdcontext.SaveChanges();
            return RedirectToPage();
        }
    }
}

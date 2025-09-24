using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaleProducts.Data;
using SaleProducts.Models;

namespace SaleProducts.Pages
{
    public class PrintModel : PageModel
    {
        private readonly AppDbContext _printcontext;
        public Product Product { get; set; }
        public PrintModel(AppDbContext printcontext)
        {
            _printcontext = printcontext;
        }

        public IActionResult OnGet(int id)
        {
            Product = _printcontext.Products.FirstOrDefault(p => p.ProductId == id);

            if (Product == null)
                return NotFound();

            return Page();
        }

    }
}

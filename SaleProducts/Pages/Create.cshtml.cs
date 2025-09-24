using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleProducts.Data;
using SaleProducts.Models;

namespace SaleProducts.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet =true)]
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }

        [BindProperty]
        public Product Product { get; set; }
        
        public List<Product> ProductList { get; set; }

        [BindProperty]

        public bool IsEditMode { get; set; }

        [BindProperty]
        public List<int> SelectedIds { get; set; } 
        public void OnGet(int PageNumber = 1)
        {
            //server-side Pagination
            CurrentPage = PageNumber;
            var totalProducts = _context.Products.Count();

            TotalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

            ProductList = _context.Products
                .OrderBy(p => p.ProductId)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            //Retreive the Data from DB
            //ProductList = _context.Products.ToList(); this line shows all the data in the table excluding the Pagination.
            Product = new Product();
            IsEditMode = false;
        }

        public IActionResult OnPost()
        {

            string fetchInput = Request.Form["FetchProductId"];

            Product = new Product(); // default reset
            IsEditMode = false;

            if (!string.IsNullOrWhiteSpace(fetchInput))
            {
                var product = _context.Products
                    .FirstOrDefault(p => p.ProductId.ToString() == fetchInput || p.Barcode == fetchInput);

                if (product != null)
                {
                    Product = product;
                    IsEditMode = true;
                }
            }

            ProductList = _context.Products.ToList();
            return Page();
           
        }
      
        public IActionResult OnPostSave()
        {
            if (Product.ProductId < 1001 || _context.Products.Any(p => p.ProductId == Product.ProductId))
            {
                var nextId = (_context.Products.Any() ? _context.Products.Max(p => p.ProductId) + 1 : 1001);
                Product.ProductId = nextId;
            }
            //check for unique Barcode
            bool barcodeExists = _context.Products.Any(p => p.Barcode == Product.Barcode);
            if (barcodeExists)
            {
                ModelState.AddModelError("Product.Barcode", "Already Exists.Enter the Correct Barcode");
                ProductList = _context.Products.ToList();
                return Page();
            }
            _context.Products.Add(Product);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostEdit(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            ProductList = _context.Products.ToList();
            IsEditMode = true;
            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            var productInDb = _context.Products.FirstOrDefault(p => p.ProductId == Product.ProductId);
            if (productInDb != null)
            {
                productInDb.ProductName = Product.ProductName;
                productInDb.SellingPrice = Product.SellingPrice;
                productInDb.Packages = Product.Packages;
                productInDb.Barcode = Product.Barcode;


                _context.SaveChanges();
            }

            return RedirectToPage();
        }

        
        public IActionResult OnPostDelete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostSelectedDelete()
        {

            if(SelectedIds != null && SelectedIds.Count > 0)
            {
                var selectedData = _context.Products.Where(p => SelectedIds.Contains(p.ProductId)).ToList();
                _context.Products.RemoveRange(selectedData);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please select at least one record.");
                return Page(); // Return the same page with the error
            }

                return RedirectToPage();
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage();
        }
    }
}

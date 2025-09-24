using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleProducts.Data;
using SaleProducts.Models;

namespace SaleProducts.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly AppDbContext _context;
        public RegistrationModel(AppDbContext context) => _context = context;

        [BindProperty]
        public User NewUser { get; set; }

        public IActionResult OnPost()
        {
            _context.LoginUsers.Add(NewUser);
            _context.SaveChanges();
            return RedirectToPage("Login");
        }
    }
}

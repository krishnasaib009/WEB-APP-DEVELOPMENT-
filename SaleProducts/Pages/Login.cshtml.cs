using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SaleProducts.Data;
using SaleProducts.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SaleProducts.Pages
{
    public class LoginModel : PageModel
    {
       
        private readonly AppDbContext _context;
        public LoginModel(AppDbContext context) => _context = context;

        [BindProperty]
        public User LoginUser { get; set; }
        public string Token { get; set; }
        public IActionResult OnPost()
        {
            var user = _context.LoginUsers.FirstOrDefault(u => 
            u.UserName == LoginUser.UserName && u.Password == LoginUser.Password);

            if (user == null) return Page();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_my_super_secret_key_1234567890_secure"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim(ClaimTypes.Name, user.UserName) };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            Token = new JwtSecurityTokenHandler().WriteToken(token);


            return RedirectToPage("/Create"); // Display token or redirect as needed
        }
    }
}

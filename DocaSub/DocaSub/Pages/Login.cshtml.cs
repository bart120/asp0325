using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocaSub.Pages
{
    public class LoginModel : PageModel
    {
        [Required]
        [EmailAddress]
        [BindProperty]
        public string Username { get; set; } = "";
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public string? Token { get; set; }


        public void OnGet()
        {
        }

        public void OnPost()
        {
            if(ModelState.IsValid)
            {
                if(Username == "toto@toto.com" && Password == "azerty")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes("unesupercledocaformation123456789!!!!!!!");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, "Toto"),
                            new Claim(ClaimTypes.Role, "Admin")
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        Issuer = "docaformation",
                        Audience = "docaformation",
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key), 
                            SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    Token = tokenHandler.WriteToken(token);
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid username or password.");
                }
            }
        }
    }

   
}

using guestForm.Data;
using Microsoft.AspNetCore.Mvc;
using guestForm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace guestForm.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _db;
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();
        private readonly IConfiguration _configuration;
        public UserController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("Signup");
        }

        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public string generateID()
        {
            return Guid.NewGuid().ToString("N").ToLower();
        }
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new object(), password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        // Check if an email is already in use
        public async Task<bool> IsEmailInUseAsync(string email)
        {
            return await _db.Users
                .AnyAsync(u => u.Email == email);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.FirstName),
            };
            var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [HttpPost]
        public IActionResult Signup(User user)
        {
            user.UserId = generateID();
            var emailExists = IsEmailInUseAsync(user.Email).Result;
            if (emailExists)
            {
                ModelState.AddModelError("Email", "Email Already in Use");
                return View(user);
            }
           
            
            user.Password = HashPassword(user.Password);
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Login");     
          
        }

        [HttpPost]
        public IActionResult Login(User u)
        {
            if(u.Password == null)
            {
                ModelState.AddModelError("Email", "Password is Required");
                return View(u);
            }
            var user = _db.Users.FirstOrDefault(x => x.Email == u.Email);
            if(user == null)
            {
                ModelState.AddModelError("Email", "Wrong Credentials");
                return View(u);
            }

            var result = VerifyPassword(user.Password, u.Password);
            if(result == false)
            {
                ModelState.AddModelError("Email", "Wrong Credentials");
                return View(u);
            }
            String token = GenerateToken(user);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMonths(1);
            options.IsEssential = true;
            options.Path = "/";

            HttpContext.Response.Cookies.Append("access_token", token,options); 


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]

        public IActionResult Signout()
        {
            HttpContext.Response.Cookies.Delete("access_token");
            return RedirectToAction("Index", "Home");
        }


    }
}

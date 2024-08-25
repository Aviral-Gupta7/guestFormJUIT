using guestForm.Data;
using Microsoft.AspNetCore.Mvc;
using guestForm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace guestForm.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _db;
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();
        public UserController(ApplicationDbContext db)
        {
            _db = db;
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

        [HttpPost]
        public IActionResult Signup(User user)
        {
            var emailExists = IsEmailInUseAsync(user.Email).Result;
            if (emailExists)
            {
                ModelState.AddModelError("Email", "Email Already in Use");
                return View(user);
            }
           
            if (ModelState.IsValid)
            {
                user.Password = HashPassword(user.Password);
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
            
          
        }
    }
}

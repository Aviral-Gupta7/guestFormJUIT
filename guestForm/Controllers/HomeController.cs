using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using guestForm.Models;
using guestForm.Data;
using System.Net.Mail;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace guestForm.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bookingform()
        {
            return View();
        }
        public IActionResult approveFormAdmin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void sendMail(BookingForm bookingform,String mailBody,String mailTo)
        {
            string fromMail = _configuration["smtpMailService:fromMail"];
            string fromMailPassword = _configuration["smtpMailService:fromMailPassword"];


            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(mailTo));
            message.Body = mailBody;
            message.IsBodyHtml = true;
            message.Subject = "Request to Book a Guest Room";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(fromMail, fromMailPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public string generateID()
        {
            return Guid.NewGuid().ToString("N").ToLower();
        }

        public static bool IsValidMobileNumber(string mobileNumber)
        {
            // Regular expression for validating a 10-digit mobile number
            string pattern = @"^\d{10}$";  // Example for validating a 10-digit number (no spaces, no special characters)

            Regex regex = new Regex(pattern);
            return regex.IsMatch(mobileNumber);
        }
        public String[] validateBookingForm(BookingForm bf)
        {
            if (bf.userId == null || bf.userId.Trim() == "")
            {
                return ["FacultyName", "USER NOT LOGGED IN"];
            }
            if (bf.FacultyName == null || bf.FacultyName.Trim() == "")
            {
                return ["FacultyName", "Invalid Faculty Name"];
            }
            //Mobile No
            if (!IsValidMobileNumber(bf.MobileNo))
            {
                return ["MobileNo", "Invalid Mobile No."];
            }
            //Date and time
            DateTime dateNow = DateTime.Now;
            if(bf.DateTimeFrom < dateNow)
            {
                return ["DateTimeFrom", "Please Enter Valid Date and Time."];
            }
            if(bf.DateTimeUpto < dateNow || bf.DateTimeUpto < bf.DateTimeFrom)
            {
                return ["DateTimeUpto", "Please Enter Valid Date and Time."];
            }
            //Relation
            if(bf.Relation == null || bf.Relation.Trim() == "")
            {
                return ["Relation", "Required Field."];
            }
            if(bf.MealsRequired == null)
            {
                return ["Relation", "Required Field."];
            }
            return ["None", "Ok"];
        }

        public String GetLoggedInUsersId(string token)
        {
            var key = _configuration["JwtSettings:key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // Validate the token and get claims principal
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,  
                    ValidateAudience = false, 
                    ClockSkew = TimeSpan.Zero  
                }, out SecurityToken validatedToken);

               
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                
                return userId;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null; 
            }
        }
    
    

        [HttpPost]

        public IActionResult Bookingform(BookingForm bookingform)
        {
                bookingform.FormId = generateID();
                bookingform.IsAdminApproved = false;
                bookingform.IsRegistrarApproved = false;
                String Usertoken = HttpContext.Request.Cookies["access_token"];
                bookingform.userId = GetLoggedInUsersId(Usertoken);
                String[] validator = validateBookingForm(bookingform);
                if(validator[1] != "Ok")
                {
                    ModelState.AddModelError(validator[0], validator[1]);
                    return View(bookingform);
                }

                _db.BookingForms.Add(bookingform);

                _db.SaveChanges();
            String mailBody = "<html> <body> " +
                $"{bookingform.FacultyName} has request to book a form" +
                $"<a href='https://localhost:7000/Approve/admin/{bookingform.FormId}'> Review the Request </a>" +
                " </body> </html>";

                String adminMail = _configuration["smtpMailService:adminMail"];
                sendMail(bookingform,mailBody,adminMail);
                return RedirectToAction();         
        }
        

    }
}
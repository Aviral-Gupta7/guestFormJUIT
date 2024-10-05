using guestForm.Data;
using guestForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Mail;

namespace guestForm.Controllers
{
    public class ApproveController : Controller
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public ApproveController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public IActionResult Succesfull()
        {
            return View();
        }

        public void sendMail(String mailBody, String mailTo)
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

        [HttpGet("Approve/admin/{id}")]
        public IActionResult admin(string id)
        {
            var form =  _db.BookingForms.FirstOrDefault(x => x.FormId == id);
            ViewBag.form = form;
            return View();
        }
        [HttpGet("Approve/registrar/{id}")]
        public IActionResult registrar(string id)
        {
            var form = _db.BookingForms.FirstOrDefault(x => x.FormId == id);
            ViewBag.form = form;
            return View();
        }

        [HttpPost("Approve/adminApprove")]
        public IActionResult approveAdmin(ApprovalModel ap)
        {
            var form = _db.BookingForms.FirstOrDefault(x => x.FormId == ap.formId);
            form.IsAdminApproved = true;
            _db.SaveChanges();
            String mailBody = "<html> <body> " +
                $"{form.FacultyName} has request to book a form" +
                $"<a href='https://localhost:7000/Approve/registrar/{form.FormId}'> Review the Request </a>" +
                " </body> </html>";

            String adminMail = _configuration["smtpMailService:registrarMail"];
            sendMail(mailBody, adminMail);
            return View("Succesfull");
        }

        [HttpPost("Approve/adminReject")]
        public IActionResult adminReject(ApprovalModel ap)
        {
            var form = _db.BookingForms.FirstOrDefault(x => x.FormId == ap.formId);
            _db.BookingForms.Attach(form);
            _db.BookingForms.Remove(form);
            _db.SaveChanges();
            var user = _db.Users.FirstOrDefault(x => x.UserId == form.userId);
            sendMail("Your Booking Form has been Rejected", user.Email);
            return View("Succesfull");
        }

        [HttpPost("Approve/registrarApprove")]
        public IActionResult registrarApprove(ApprovalModel ap)
        {
            var form = _db.BookingForms.FirstOrDefault(x => x.FormId == ap.formId);
            form.IsRegistrarApproved = true;
            _db.SaveChanges();
            var user = _db.Users.FirstOrDefault(x => x.UserId == form.userId);
            sendMail("Your Booking Form has been Approved", user.Email);
            return View("Succesfull");
        }

        [HttpPost("Approve/registrarReject")]
        public IActionResult registrarReject(ApprovalModel ap)
        {
            var form = _db.BookingForms.FirstOrDefault(x => x.FormId == ap.formId);
            _db.BookingForms.Attach(form);
            _db.BookingForms.Remove(form);
            _db.SaveChanges();
            var user = _db.Users.FirstOrDefault(x => x.UserId == form.userId);
            sendMail("Your Booking Form has been Rejected", user.Email);
            return View("Succesfull");
        }
    }
}

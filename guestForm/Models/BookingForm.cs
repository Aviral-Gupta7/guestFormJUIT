using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace guestForm.Models
{
    public class BookingForm
    {
        [Key]
        public String? FormId { get; set; }

        [ForeignKey("User")]
        public String? userId { get; set; }

        [Required]
        public String? FacultyName { get; set; }
        [Required]
        public String? MobileNo { get; set; }
        [Required]
        public String? Department { get; set; }
        [Required]
        public DateTime DateTimeFrom { get; set; }
        [Required]
        public DateTime DateTimeUpto { get; set; }
        [Required]
        public int GuestNumMaleSin { get; set; }
        [Required]
        public int GuestNuFemaleSin { get; set; }
        [Required]
        public int GuestNumCouple { get; set; }
        [Required]
        public int GuestNumChildren { get; set; }
        [Required]
        public String? Relation { get; set; }
        [Required]
        public bool MealsRequired { get; set; }
        [Required]
        public bool IsAdminApproved { get; set; }
        [Required]
        public bool IsRegistrarApproved { get; set; }

        
    }
}

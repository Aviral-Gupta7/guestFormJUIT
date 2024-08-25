

using System.ComponentModel.DataAnnotations;


namespace guestForm.Models
{
    public class BookingForm
    {
        [Key]
        public Guid FormId { get; set; }

        // will be added after login is implemented

        //[ForeignKey("UserId")]
        //public String? UserId { get; set; }

        [Required]
        public String? FacultyName { get; set; }
        [Required]
        public int MobileNo { get; set; }
        [Required]
        public String? Department { get; set; }
        [Required]
        public int GuestNuMaleSi { get; set; }
        [Required]
        public int GuestNuFemaleSi { get; set; }
        [Required]
        public int GuestNuCouple { get; set; }
        [Required]
        public int GuestNuChildren { get; set; }
        public DateTime From { get; set; }
        public DateTime Upto { get; set; }
        [Required]
        public String? RelationWithRequisitionerr { get; set; }
        public bool IsAdminApproved { get; set; }
        public bool IsRegistrarApproved { get; set; }

    }
}

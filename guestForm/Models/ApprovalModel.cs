using System.ComponentModel.DataAnnotations;

namespace guestForm.Models
{
    public class ApprovalModel
    {
        [Required]
        public String formId { get; set; }
    }
}

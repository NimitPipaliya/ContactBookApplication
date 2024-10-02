using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactBookApplication.ViewModels
{
    public class ContactViewModel
    {

        public int ContactId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(15)]
        [DisplayName("First name")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(15)]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15)]
        [DisplayName("Phone number")]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid contact number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email id is required.")]
        [StringLength(50)]
        [DisplayName("Email id")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255)]
        [DisplayName("Address")]
        public string Address { get; set; }

        public string FileName { get; set; } = string.Empty;

        [DisplayName("File is Required")]
        public IFormFile File { get; set; }
    }
}

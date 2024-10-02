using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ContactBookApplication.Models
{
    public class ContactBook
    {
        [Key]
        [DisplayName("Contact id")]
        public int ContactId { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email id")]
        public string EmailId { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Address")]
        public string Address { get; set; }

        public string FileName { get; set; } = string.Empty;

    }
}

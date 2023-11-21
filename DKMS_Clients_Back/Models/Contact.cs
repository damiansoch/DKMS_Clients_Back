using System.ComponentModel.DataAnnotations;

namespace DKMS_Clients_Back.Models
{
    public class Contact
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? PhoneNumber2 { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(200)]
        [EmailAddress]
        public string? Email2 { get; set; }
        public string? ExtraDetails { get; set; }

    }
}

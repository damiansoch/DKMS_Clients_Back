using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Dtos
{
    public class AddCustomerRequestDto
    {
        private readonly string _firstName;

        //for customer
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? CompanyName { get; set; }

        //for contact
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

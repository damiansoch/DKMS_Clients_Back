using System.ComponentModel.DataAnnotations;

namespace DKMS_Clients_Back.Dtos
{
    public class UpdateCustomerRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? CompanyName { get; set; }
    }
}

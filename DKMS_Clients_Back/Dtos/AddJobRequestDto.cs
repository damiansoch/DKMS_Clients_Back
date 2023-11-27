using System.ComponentModel.DataAnnotations;

namespace DKMS_Clients_Back.Dtos
{
    public class AddJobRequestDto
    {
       
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Deposit { get; set; } = 0m;
        [Required]
        public DateTime ToBeCompleted { get; set; }

    }
}

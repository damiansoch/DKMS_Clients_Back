using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMS_Clients_Back.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }=string.Empty;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? CompanyName { get; set; }

        [NotMapped]
        public List<Contact>? Contacts { get; set; } = new List<Contact>();
        [NotMapped]
        public List<Address>? Addresses { get; set; } = new List<Address>();
        [NotMapped]
        public List<Job>? Jobs { get; set; } = new List<Job>();
    }
}

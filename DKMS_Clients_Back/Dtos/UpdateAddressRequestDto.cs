using System.ComponentModel.DataAnnotations;

namespace DKMS_Clients_Back.Dtos
{
    public class UpdateAddressRequestDto
    {
        public int? HouseNumber { get; set; }
        [MaxLength(100)]
        public string? HouseName { get; set; }
        [MaxLength(250)]
        public string? AddressLine1 { get; set; }
        [MaxLength(250)]
        public string? AddressLine2 { get; set; }
        [MaxLength(250)]
        public string? AddressLine3 { get; set; }
        [MaxLength(50)]
        public string? EirCode { get; set; }
    }
}

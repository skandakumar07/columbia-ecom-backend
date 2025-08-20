using System.ComponentModel.DataAnnotations;

namespace EcommerceTrail.DTO
{
    public class AddressBookDto
    {
        [Required]
        public int? UsersId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string AddressLine { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}

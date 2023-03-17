using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Company name is a required field.")]
        [MaxLength(60, ErrorMessage ="Maximum length for the name is 60 character.")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Company address is a required field.")]
        [MaxLength(60, ErrorMessage ="Maximum length for the Address is 60 character.")]
        public string? Address { get; set; }

        public string? Country { get; set; }

        public ICollection<Employee>? Emplyees { get; set; }
    }
}

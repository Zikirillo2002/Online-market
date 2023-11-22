using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Lesson11.Models
{
    [Table(nameof(Customer))]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [MaxLength(255)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}

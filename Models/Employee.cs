using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public required byte[] ProfilePicture { get; set; }
    }
}

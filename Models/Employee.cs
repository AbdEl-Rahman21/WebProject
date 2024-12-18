using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public byte[]? ProfilePicture { get; set; }

        public Employee(string Name, string PhoneNumber,  int Age, byte[] ProfilePicture)
        {
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.Age = Age;
            this.ProfilePicture = ProfilePicture;
        }
    }
}

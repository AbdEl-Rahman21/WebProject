using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebProject.Models
{
    [method: SetsRequiredMembers]
    public class Employee(int Id, string Name, string PhoneNumber, int Age, byte[] ProfilePicture)
    {
        [Key]
        public int Id { get; set; } = Id;

        [Required]
        public required string Name { get; set; } = Name;

        [Required]
        public required string PhoneNumber { get; set; } = PhoneNumber;

        [Required]
        public int Age { get; set; } = Age;

        [Required]
        public required byte[] ProfilePicture { get; set; } = ProfilePicture;
    }
}

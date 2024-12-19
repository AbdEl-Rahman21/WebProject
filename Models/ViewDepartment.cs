using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebProject.Models
{
    public class ViewDepartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }

        [Required]
        public int Age { get; set; }

        public ViewDepartment() { }

        [SetsRequiredMembers]
        public ViewDepartment(Department department)
        {
            Id = department.Id;
            Name = department.Name;
            PhoneNumber = department.PhoneNumber;
            Age = department.Age;
        }

        public Department MapToDepartment()
        {
            return new Department(Id, Name, PhoneNumber, Age);
        }
    }
}

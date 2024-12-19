using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebProject.Models
{
    public class ViewEmployee
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

        [DataType(DataType.Upload)]
        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePicture { get; set; }

        public ViewEmployee() { }

        [SetsRequiredMembers]
        public ViewEmployee(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            PhoneNumber = employee.PhoneNumber;
            Age = employee.Age;

            MemoryStream memoryStream = new(employee.ProfilePicture);

            ProfilePicture = new FormFile(memoryStream, 0, memoryStream.Length, "Profile Picture", "ProfilePicture")
            {
                Headers = new HeaderDictionary()
            };
        }

        public async Task<Employee> MapToEmployeeAsync()
        {
            if (ProfilePicture == null)
            {
                throw new InvalidOperationException("Profile Picture can not be null when creating new employees!");
            }
            else
            {
                using MemoryStream memoryStream = new();

                await ProfilePicture.CopyToAsync(memoryStream);

                return new Employee(Id, Name, PhoneNumber, Age, memoryStream.ToArray());
            }
        }

        public async Task<string> GetProfilePictureAsync()
        {
            if (ProfilePicture == null)
            {
                return string.Empty;
            }
            else
            {
                using MemoryStream memoryStream = new();

                await ProfilePicture.CopyToAsync(memoryStream);

                return string.Format("data:image;base64," + Convert.ToBase64String(memoryStream.ToArray()));
            }
        }
    }
}

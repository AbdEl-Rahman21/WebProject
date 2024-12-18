﻿using Microsoft.AspNetCore.Http;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class ViewEmployee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePicture { get; set; }

        public Employee MapToEmployee()
        {
            using var memoryStream = new MemoryStream();
            ProfilePicture.CopyTo(memoryStream);

            Employee employee = new Employee(Name, PhoneNumber, Age, memoryStream.ToArray());

            return employee;
        }
    }
}

using LearningCoreWeb.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningCoreWeb.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Please provide a value for Name field")]
        public string Name { get; set; }

        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        [Required(ErrorMessage = "Please provide a value for Email field")]
        public string Email { get; set; }
        public DepartmentEnum Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}

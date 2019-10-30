using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationDI.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the First name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Last name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                ErrorMessage = "Email is not valid")]
        public string Email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the phone number")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "Length must be 10")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string PhoneNumber { get; set; }
        //public Nullable<bool> Status { get; set; }
        public bool Status { get; set; }

    }
}

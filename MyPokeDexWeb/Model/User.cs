using System;
using System.ComponentModel.DataAnnotations;

namespace MyPokeDexWeb.Model
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Password must be at least 10 characters long.")]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{10,}$", ErrorMessage = "Password must contain at least one number, and a mix of upper-case and lower-case letters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public int RoleID { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}

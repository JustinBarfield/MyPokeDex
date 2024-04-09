using System.ComponentModel.DataAnnotations;

namespace MyPokeDexWeb.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        [Required(ErrorMessage ="First Name is required")]
        [Display(Name = "First Name ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password is required")]
		[Display(Name = "Password ")]
		public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }
        public int RoleID { get; set; }

        public DateTime LastLoginTime { get; set; }


    }
}

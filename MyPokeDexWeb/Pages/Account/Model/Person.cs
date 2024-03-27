namespace MyPokeDexWeb.Pages.Account.Model
{
	public class Person
	{
		public int PersonId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int RoleID { get; set; }

		public DateTime LastLoginTime { get; set; }


	}
}

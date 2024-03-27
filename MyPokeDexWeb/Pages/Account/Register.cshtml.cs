using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyPokeDexWeb.Pages.Account
{
    public class RegisterModel : PageModel
    {
		[BindProperty]
		public Person NewPerson { get; set; }
		public void OnGet()
        {
          // NewPerson.FirstName = " Please enter your first name.";
        }
        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //insert data into databse
                //1. Create a database connection string
                string connString = "Server=(localdb)\\MSSQLLocalDB;Database=MyPokedex;Trusted_Connection=true;";
                SqlConnection conn = new SqlConnection(connString);
                //2. Create a insert command
                string cmdText = " INSERT INTO Person(FirstName, LastName, Email,Password, phone, RoleID, )" + 
                    "Values(fFirstName, @lastName, @email, @password, @telephone, 2, @lastLogInTime, roleID)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@firstName", NewPerson.FirstName);
				cmd.Parameters.AddWithValue("@lastName", NewPerson.LastName);
				cmd.Parameters.AddWithValue("@password", NewPerson.Password);
				cmd.Parameters.AddWithValue("@email", NewPerson.email);
				cmd.Parameters.AddWithValue("@telephone", NewPerson.Telephone);
				cmd.Parameters.AddWithValue("@roleid", NewPerson.RoleID);
				cmd.Parameters.AddWithValue("@lastLoginTime", DateTime.Now.ToString());
                //3. open the database
                conn.Open();
                //4. execute the command
                cmd.ExecuteNonQuery();
				//5. Close the database
                conn.Close();

                return RedirectToPage("login");
			}
            return Page();
        
        }
    }
}

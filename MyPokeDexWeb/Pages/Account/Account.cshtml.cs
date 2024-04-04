using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Person NewPerson { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Create database connection
                string connectionString = SecurityHelper.GetDBConnectionString();

                // Open connection and insert data
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // SQL insert command with parameters
                    string cmdText = "UPDATE Person(FirstName, LastName, Email, Password, Phone, RoleID) " +
                                     "VALUES (@firstName, @lastName, @email, @password, @telephone, @roleID)";

                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        // Add parameters to SQL command
                        cmd.Parameters.AddWithValue("@firstName", NewPerson.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", NewPerson.LastName);
                        cmd.Parameters.AddWithValue("@password", SecurityHelper.GeneratePasswordHash(NewPerson.Password));
                        cmd.Parameters.AddWithValue("@email", NewPerson.Email);
                        cmd.Parameters.AddWithValue("@telephone", NewPerson.Phone);
                        cmd.Parameters.AddWithValue("@roleID", 2); // Assuming a default role


                        // Open connection and execute SQL command
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redirect to Profile page after successful registration
                return RedirectToPage("Profile");
            }

            // Return page if model state is not valid
            return Page();
        }
    }
}
    


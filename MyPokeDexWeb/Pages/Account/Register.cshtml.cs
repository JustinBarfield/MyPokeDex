using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Person NewPerson { get; set; }

        public void OnGet()
        {
            // Initialize if needed
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Make sure the email is not already in the table

                if (EmailDNE(NewPerson.Email))
                {
                    RegisterUser();
                    return RedirectToPage("Login");

                }
                else
                {
                    ModelState.AddModelError("Register Error", "The email already exists. Try a different one");
                    return Page();
                }

                // Create database connection
                string connectionString = SecurityHelper.GetDBConnectionString();

               

                // Redirect to Profile page after successful registration
                return RedirectToPage("Login");
            }

            // Return page if model state is not valid
            return Page();
        }

        private void RegisterUser()
        {
            using(SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "INSERT INTO Person(FirstName, LastName, Email, Password, Phone, RoleID) " +
                                    "VALUES (@firstName, @lastName, @email, @password, @telephone, @roleID)";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
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

        private bool EmailDNE(string email)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT * FROM Person WHERE Email=@email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("email", email);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
    }
}

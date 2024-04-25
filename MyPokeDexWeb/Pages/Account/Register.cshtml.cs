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
            // Initialize any additional resources if necessary
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists in the table
                if (EmailDNE(NewPerson.Email))
                {
                    // Register user and redirect to the login page
                    RegisterUser();
                    return RedirectToPage("Login");
                }
                else
                {
                    // Add error if email already exists
                    ModelState.AddModelError("Email", "The email already exists. Please try a different one.");
                    return Page();
                }
            }
            // Return the same page with validation errors if the model state is not valid
            return Page();
        }

        private void RegisterUser()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "INSERT INTO Person (FirstName, LastName, Email, Password, Phone, RoleID) " +
                                 "VALUES (@firstName, @lastName, @Email, @Password, @Phone, @RoleID)";

                SqlCommand cmd = new SqlCommand(cmdText, conn);

                // Use parameterized queries to add parameters safely
                cmd.Parameters.AddWithValue("@firstName", NewPerson.FirstName);
                cmd.Parameters.AddWithValue("@lastName", NewPerson.LastName);
                cmd.Parameters.AddWithValue("@Email", NewPerson.Email);
                cmd.Parameters.AddWithValue("@Password", SecurityHelper.GeneratePasswordHash(NewPerson.Password));
                cmd.Parameters.AddWithValue("@Phone", NewPerson.Phone);
                cmd.Parameters.AddWithValue("@RoleID", 2); // Default role ID

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

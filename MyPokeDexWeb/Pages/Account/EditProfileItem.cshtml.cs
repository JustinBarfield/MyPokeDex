using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPokeDexWeb.Model;
using System.Data.SqlClient;

namespace MyPokeDexWeb.Pages.Account
{
    public class EditProfileItemModel : PageModel
    {
        [BindProperty]
        public Person profile { get; set; } = new Person();

        public void OnGet(int id)
        {
            // Load the profile from the database based on the provided ID
            LoadProfile(id);
        }

        private void LoadProfile(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT PersonId, FirstName, LastName, Email, Phone, Password, LastLoginTime FROM Person WHERE PersonId = @id";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    profile.PersonId = reader.GetInt32(0);
                    profile.FirstName = reader.GetString(1);
                    profile.LastName = reader.GetString(2);
                    profile.Email = reader.GetString(3);
                    profile.Phone = reader.GetString(4);
                    profile.Password = reader.GetString(5);
                    profile.LastLoginTime = reader.GetDateTime(6);
                }
            }
        }

        public IActionResult OnPost(int id)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return Page(); // Return the page with validation errors
            }

            // Perform the update operation
            UpdateProfile(id);

            // Redirect to the profile view page upon successful update
            return RedirectToPage("/Account/Profile");
        }

        private void UpdateProfile(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Password = @Password WHERE PersonId = @id";

                SqlCommand cmd = new SqlCommand(cmdText, conn);

                // Bind parameter values from the profile object
                cmd.Parameters.AddWithValue("@FirstName", profile.FirstName);
                cmd.Parameters.AddWithValue("@LastName", profile.LastName);
                cmd.Parameters.AddWithValue("@Email", profile.Email);
                cmd.Parameters.AddWithValue("@Phone", profile.Phone);
                cmd.Parameters.AddWithValue("@Password", SecurityHelper.GeneratePasswordHash(profile.Password));
                cmd.Parameters.AddWithValue("@id", id);

                // Open the connection and execute the command
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}


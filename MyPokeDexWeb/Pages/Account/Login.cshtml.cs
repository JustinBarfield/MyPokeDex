using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
		public Login LoginUser { get; set; }
		public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //check login credentials
                if (ValidateCredentials())
                {
                    return RedirectToPage("Profile");
                }
                else
                {
                    ModelState.AddModelError("Login Eror", "Invalid Credentials, Try Again.");
                    return Page();
                }
                //if credentials are valid



                //redirect User to Profile

                //otherwise, display error

            }
            else
            {
                return Page();
            }
        }

        private bool ValidateCredentials()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT Password, PersonID, FirstName, LastName, Email, Roles.RoleName " +
                                 "FROM Person " +
                                 "INNER JOIN Roles ON Person.RoleID = Roles.RoleID " +
                                 "WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@Email", LoginUser.Email);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve the stored password hash and verify it with the provided password
                            string passwordHash = reader.GetString(0);
                            if (SecurityHelper.VerifyPassword(LoginUser.Password, passwordHash))
                            {
                                // Retrieve the PersonID
                                int personID = reader.GetInt32(1);
                                UpdatePersonLoginTime(personID);

                                // Retrieve the necessary data for claims
                                string name = reader.GetString(2);
                                string roleName = reader.GetString(5);

                                // Create claims list
                                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, LoginUser.Email),
                            new Claim(ClaimTypes.Name, name),
                            new Claim(ClaimTypes.Role, roleName)
                        };

                                // Create a ClaimsIdentity and add it to a ClaimsPrincipal
                                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                                // Sign in using the principal
                                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur
                    // For example: log the exception details for debugging
                    Console.Error.WriteLine($"Error in ValidateCredentials: {ex.Message}");
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }



        private void UpdatePersonLoginTime(int personID)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "UPDATE Person SET LastLoginTime=@lastLoginTime Where PersonID=@personId";
                SqlCommand cmd = new SqlCommand( cmdText, conn);
                cmd.Parameters.AddWithValue("@lastLoginTime",DateTime.Now);
                cmd.Parameters.AddWithValue("personId", personID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

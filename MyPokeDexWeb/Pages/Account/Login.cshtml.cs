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
                string cmdText = "SELECT Password, PersonID,FirstName, LastName Email FROM Person " +"" +
                    " INNER JOIN [Roles] ON Person.RoleID = [ROLEs].RoleID WHERE Email=@email"; 
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                // Add the @email parameter
                cmd.Parameters.AddWithValue("@email", LoginUser.Email);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))//the 0 is to select the correct index
                    {
                        string passwordHash = reader.GetString(0);
                        if (SecurityHelper.VerifyPassword(LoginUser.Password, passwordHash))
                        {
                            //get the PersonID and use it to update the Person record
                            int personID = reader.GetInt32(1);
                            UpdatePersonLoginTime(personID);

                            //create a principle
                            string name = reader.GetString(2);
                            string roleName = reader.GetString(3);

                            //create list of claims
                            Claim emailClaim = new Claim(ClaimTypes.Email, LoginUser.Email);
                            Claim nameClaim = new Claim(ClaimTypes.Name, name);
                            Claim roleClaim = new Claim(ClaimTypes.Role, roleName);

                            List<Claim> claims = new List<Claim> { emailClaim, nameClaim, roleClaim }; 

                            //2. add the list of claims to a claims Identity

                            ClaimsIdentity Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            //3. add the identity to a ClaimsPrinciple
                            ClaimsPrincipal principle = new ClaimsPrincipal(Identity);

                            //4. call HTTPContext.SignInAsync() method to encrypt the principal
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);






                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                conn.Close();
            }
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

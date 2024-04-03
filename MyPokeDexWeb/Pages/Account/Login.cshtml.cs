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
                    ModelState.AddModelError("Login Eror", "invalid credential, try again.");
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
                string cmdText = "SELECT Password,PersonID FROM  Person WHERE  Email=@email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))//the 0 is to selecct the correct index
                    {
                        string passwordHash = reader.GetString(0);
                        if (SecurityHelper.VerifyPassword(LoginUser.Password, passwordHash))
                        {
                            //get the PersonID and use it to update the Person record
                            int personID = reader.GetInt32(1);
                            UpdatePersonLoginTime(personID);

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

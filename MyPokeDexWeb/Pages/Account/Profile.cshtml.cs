using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public Person profile { get; set; } = new Person();
        public void OnGet()
        {
            PopulateProfile();


        }

		private void PopulateProfile()
		{
            string email = HttpContext.User.FindFirstValue(ClaimValueTypes.Email);
            using(SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT FirstName, LastName, Email,Phone, LastLoginTime, PersonId FROM Person WHERE Email=@email ";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@email",email);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    profile.FirstName = reader.GetString(0);
                    profile.LastName = reader.GetString(1);
                    profile.Email = reader.GetString(2);    
                    profile.Phone = reader.GetString(3);
                    profile.LastLoginTime = reader.GetDateTime(4);
                    profile.PersonId = reader.GetInt32(5);
                }


            }
			
		}
	}
}

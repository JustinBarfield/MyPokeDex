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
        public UserProfile profile { get; set; }
        public void OnGet()
        {
            PopulateProfile();


        }

		private void PopulateProfile()
		{
            string email = HttpContext.User.FindFirstValue(ClaimValueTypes.Email);
            using(SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT FIrstName, LastName, Email,Phone, LastLoginTime FROM Person WHERE Email=@email ";
                cmd.Parameters.AddWithValue("@email",email);
                conn.Open();
                SqlDataReader;


            }
			
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;

namespace MyPokeDexWeb.Pages.Account
{
    public class DeleteUserItemsModel : PageModel
    {
        

        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "DELETE FROM  Person WHERE PersonID = @itemid";
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                cmd.Parameters.AddWithValue("@itemID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("DeleteUsers");
            }
        }
    }
}

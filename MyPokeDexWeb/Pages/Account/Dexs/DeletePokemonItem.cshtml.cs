using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account.Dexs
{
    public class DeletePokemonModel : PageModel
    {




        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "DELETE FROM  Pokemon WHERE PokemonID = @itemid";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                
                cmd.Parameters.AddWithValue("@itemID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("ViewDexItems");
            }
        }
    }
}

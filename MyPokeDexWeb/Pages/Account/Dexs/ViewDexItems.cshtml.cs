using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;


namespace MyPokeDexWeb.Pages.Account.Dexs
{
	[BindProperties]
	public class ViewDexItemsModel : PageModel
    {
        

        public List<SelectListItem> Region { get; set; } = new List<SelectListItem>();

        public List<PokemonItem> PokemonItems { get; set; } = new List<PokemonItem>();

        public int SelectedRegionID { get; set; }
                
        public void OnGet()
        {
            PopulateRegionDDL();
			PopulateTypeDDL();

        }

        private void PopulateTypeDDL()
        {
           
        }

        private void PopulateRegionDDL()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "Select RegionID, RegionName From Region ORDER BY RegionName";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{

					while (reader.Read())
					{
						var region = new SelectListItem();
						region.Value = reader.GetInt32(0).ToString();
						region.Text = reader.GetString(1);
						Region.Add(region);
					}

				}
			}
		}
	}
}

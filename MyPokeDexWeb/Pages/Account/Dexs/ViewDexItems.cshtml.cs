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
        

        public List<SelectListItem> RegionID { get; set; } = new List<SelectListItem>();

        public List<PokemonItem> PokemonItems { get; set; } = new List<PokemonItem>();

        public int SelectedRegionID { get; set; }
                
        public void OnGet()
        {
            PopulateRegionDDL();
			PopulateTypeDDL();

        }

		public void OnPost()
		{
			PopulatDexItem(SelectedRegionID);
		}

		private void PopulatDexItem(int id)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = " SELECT PokemonID, [Dex Number], Type, [State Total], [image URL], Region, Height, Weight, Audio FROM Pokemon WHERE RegionID = @regionID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@RegionID", id);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{

					while (reader.Read())
					{
						var item = new PokemonItem();
						item.PokemonID = reader.GetInt32(0);
						item.DexNumber = reader.GetInt32(1);
						item.Type = reader.GetString(2);
						item.StateTotal = reader.GetInt32(3);
						item.ImageURL = reader.GetString(4);
						item.RegionID = reader.GetString(5);
						item.Height= reader.GetString(6);
						item.Weight = reader.GetString(7);
						item.Audio = reader.GetString(8);

						PokemonItems.Add(item);
					}

				}

			}
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
						RegionID.Add(region);
					}

				}
			}
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account.Dexs
{
    [BindProperties]
    public class AddPokemonModel : PageModel
    {
       
        public PokemonItem NewPokemon {  get; set; } = new PokemonItem();

        public List<SelectListItem> RegionID { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> Type { get; set; } = new List<SelectListItem>();

		public void OnGet()
        {
            PopulateRegionDDL();
            PopulateTypeDDL();


        }

		public IActionResult OnPost()
		{

			PopulateRegionDDL();
			PopulateTypeDDL();

			if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{


					string cmdText = "INSERT INTO  Pokemon(PokemonID, [Dex Number], Name, TypeID, [State Total], [image URL], RegionID, Height, Weight, Audio)" +
						"VALUES (@pokemonID, @dexNumber, @name, @type, @stateTotal, @imageURL, @region, @height, @weight,@audio)";
					SqlCommand cmd = new SqlCommand(cmdText, conn);
					cmd.Parameters.AddWithValue("@pokemonID", NewPokemon.PokemonID);
					cmd.Parameters.AddWithValue("@dexNUmber", NewPokemon.DexNumber);
					cmd.Parameters.AddWithValue("@name", NewPokemon.Name);
					cmd.Parameters.AddWithValue("@type", NewPokemon.TypeID);
					cmd.Parameters.AddWithValue("@stateTotal", NewPokemon.StateTotal);
					cmd.Parameters.AddWithValue("@imageURL", NewPokemon.ImageURL);
					cmd.Parameters.AddWithValue("@region", NewPokemon.RegionID);
					cmd.Parameters.AddWithValue("@height", NewPokemon.Height);
					cmd.Parameters.AddWithValue("@weight", NewPokemon.Weight);
					cmd.Parameters.AddWithValue("@audio", NewPokemon.Audio);

					conn.Open();
					cmd.ExecuteNonQuery();
					return RedirectToPage("ViewDexItems");
				}
			}
			else{
				return Page();

			}
		}

		private void PopulateTypeDDL()
		{
			using(SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
					{
				string cmdText = "Select TypeID, TypeName From Type";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{

					while(reader.Read())
					{
						var type = new SelectListItem();
						type.Value = reader.GetInt32(0).ToString();
						type.Text = reader.GetString(1);
						Type.Add(type);
					}
					
				}
			}
		}

		private void PopulateRegionDDL()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "Select RegionID, RegionName From Region";
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

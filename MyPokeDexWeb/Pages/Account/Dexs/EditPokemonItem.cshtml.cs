using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;
using static MyPokeDexWeb.Pages.Account.Dexs.ViewDexItemsModel;

namespace MyPokeDexWeb.Pages.Account.Dexs
{
    [Authorize]
    [BindProperties]
    public class EditPokemonItemModel : PageModel
    {

        public PokemonItem Pokemon { get; set; } = new PokemonItem();

        public List<SelectListItem> RegionID { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Type { get; set; } = new List<SelectListItem>();

        public List<CategoryInfo> categories { get; set; } = new List<CategoryInfo>();

        public List<int> selectedCategoryIds { get; set; }
       

      

        public void OnGet(int id)
        {
            int pokemonId = id;
            PoplatePokedexInfo(pokemonId);
            PopulateCategoryList();
            MarkCategorySelections(pokemonId);
            PopluateDexItem(id);
            PopulateRegionDDL();
            PopulateTypeDDL();
            

        }

        private void MarkCategorySelections(int pokemonId)
        {
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT CategoryId FROM Category WHERE PokemonId = @PokemonId";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("PokemonId", pokemonId);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					int categoryId = -1;
					while (reader.Read())
					{
						foreach (CategoryInfo category in categories)
						{
							categoryId = reader.GetInt32(0);
							if (categoryId == category.CategoryId)
							{
								category.isSelected = true;
								break;
							}
						}
					}
				}
			}
		}

		private void PoplatePokedexInfo(int pokemonId)
		{
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT CategoryId FROM Category WHERE PokemonId = @PokemonId";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("PokemonId", pokemonId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    int categoryId = -1;
                    while (reader.Read())
                    {
                        foreach(CategoryInfo category in categories)
                        {
                            categoryId = reader.GetInt32(0);
                            if(categoryId == category.CategoryId)
                            {
                                category.isSelected = true;
                                break;
                            }
                        }
                    }
                }
            }
		}

		private void PopulateCategoryList()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{

				string cmdText = ("SELECT CategoryID, CategoryName FROM Category");
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						CategoryInfo item = new CategoryInfo();
						item.CategoryId = reader.GetInt32(0);
						item.CategoryName = reader.GetString(1);
						item.isSelected = false;
						categories.Add(item);
					}
				}
			}
		}

		public IActionResult OnPost(int id)
        {
            PopluateDexItem(id);
            PopulateRegionDDL();
            PopulateTypeDDL();
            
            if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{
					string cmdText = "UPDATE  Pokemon SET PokemonID=@pokemonID, [Dex Number]=@dexNumber, Name=@name, TypeID=@type, [State Total]=@stateTotal," +
						"[image URL]=@imageURL, RegionID=@region, Height=@height, Weight =@weight, Audio=@audio " +
						"WHERE PokemonID = @itemid";
					SqlCommand cmd = new SqlCommand(cmdText, conn);
					cmd.Parameters.AddWithValue("@pokemonID", Pokemon.PokemonID);
					cmd.Parameters.AddWithValue("@dexNUmber", Pokemon.DexNumber);
					cmd.Parameters.AddWithValue("@name", Pokemon.Name);
					cmd.Parameters.AddWithValue("@type", Pokemon.TypeID);
					cmd.Parameters.AddWithValue("@stateTotal", Pokemon.StateTotal);
					cmd.Parameters.AddWithValue("@imageURL", Pokemon.ImageURL);
					cmd.Parameters.AddWithValue("@region", Pokemon.RegionID);
					cmd.Parameters.AddWithValue("@height", Pokemon.Height);
					cmd.Parameters.AddWithValue("@weight", Pokemon.Weight);
					cmd.Parameters.AddWithValue("@audio", Pokemon.Audio);
					cmd.Parameters.AddWithValue("@itemID", id);

					conn.Open();
					cmd.ExecuteNonQuery();
					return RedirectToPage("ViewDexItems");
				}
			}
			else
			{
				return Page();

			}
		}

        private void PopulateTypeDDL()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "Select TypeID, TypeName From Type";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
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

        private void PopluateDexItem(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT PokemonID, [Dex Number], Name, TypeID, [State Total], [image URL], RegionID, Height, Weight, Audio FROM Pokemon WHERE PokemonID = @itemID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@itemID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Pokemon.PokemonID = id;
                    Pokemon.PokemonID = reader.GetInt32(0);
                    Pokemon.DexNumber = reader.GetInt32(1);
                    Pokemon.Name = reader.GetString(2);
                    Pokemon.TypeID = reader.GetInt32(3);
                    Pokemon.StateTotal = reader.GetInt32(4);
                    Pokemon.ImageURL = reader.GetString(5);
                    Pokemon.RegionID = reader.GetInt32(6);
                    Pokemon.Height = reader.GetString(7);
                    Pokemon.Weight = reader.GetString(8);
                    Pokemon.Audio = reader.GetString(9);

                }

            }
        }
    }
    public class CategoryInfo
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool isSelected { get; set; }

    }
}

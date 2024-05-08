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
                string cmdText = "SELECT CategoryID, CategoryName FROM Category";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo item = new CategoryInfo();

                        // Retrieve CategoryID (always an integer, so no null check needed here)
                        item.CategoryId = reader.GetInt32(0);

                        // Check for NULL values in CategoryName column
                        if (!reader.IsDBNull(1))
                        {
                            // Retrieve CategoryName if not NULL
                            item.CategoryName = reader.GetString(1);
                        }
                        else
                        {
                            // Handle the case where CategoryName is NULL (set a default value or leave blank)
                            item.CategoryName = string.Empty; // You can adjust this according to your needs
                        }

                        item.isSelected = false; // Set default value for isSelected
                        categories.Add(item);
                    }
                }
            }
        }

        public IActionResult OnPost(int id)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                    {
                        // Begin a transaction to handle the update of both tables
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Update the Pokemon table
                            string updatePokemonQuery = @"
                        UPDATE Pokemon
                        SET [Dex Number] = @dexNumber,
                            Name = @name,
                            TypeID = @type,
                            [State Total] = @stateTotal,
                            [Image URL] = @imageURL,
                            RegionID = @region,
                            Height = @height,
                            Weight = @weight,
                            Audio = @audio
                        WHERE PokemonID = @pokemonID;
                    ";
                            SqlCommand cmdPokemon = new SqlCommand(updatePokemonQuery, conn, transaction);
                            cmdPokemon.Parameters.AddWithValue("@dexNumber", Pokemon.DexNumber);
                            cmdPokemon.Parameters.AddWithValue("@name", Pokemon.Name);
                            cmdPokemon.Parameters.AddWithValue("@type", Pokemon.TypeID);
                            cmdPokemon.Parameters.AddWithValue("@stateTotal", Pokemon.StateTotal);
                            cmdPokemon.Parameters.AddWithValue("@imageURL", Pokemon.ImageURL);
                            cmdPokemon.Parameters.AddWithValue("@region", Pokemon.RegionID);
                            cmdPokemon.Parameters.AddWithValue("@height", Pokemon.Height);
                            cmdPokemon.Parameters.AddWithValue("@weight", Pokemon.Weight);
                            cmdPokemon.Parameters.AddWithValue("@audio", Pokemon.Audio);
                            cmdPokemon.Parameters.AddWithValue("@pokemonID", id);
                            cmdPokemon.ExecuteNonQuery();

                            // Delete existing category associations for the Pokemon
                            string deletePokemonCategoriesQuery = @"
                        DELETE FROM Category
                        WHERE PokemonID = @pokemonID;
                    ";
                            SqlCommand cmdDeleteCategories = new SqlCommand(deletePokemonCategoriesQuery, conn, transaction);
                            cmdDeleteCategories.Parameters.AddWithValue("@pokemonID", id);
                            cmdDeleteCategories.ExecuteNonQuery();

                            // Insert new category associations for the Pokemon
                            foreach (int categoryId in selectedCategoryIds)
                            {
                                string insertPokemonCategoryQuery = @"
                            INSERT INTO Category (PokemonID, CategoryID)
                            VALUES (@pokemonID, @categoryId);
                        ";
                                SqlCommand cmdInsertCategory = new SqlCommand(insertPokemonCategoryQuery, conn, transaction);
                                cmdInsertCategory.Parameters.AddWithValue("@pokemonID", id);
                                cmdInsertCategory.Parameters.AddWithValue("@categoryId", categoryId);
                                cmdInsertCategory.ExecuteNonQuery();
                            }

                            // Commit the transaction
                            transaction.Commit();

                            return RedirectToPage("ViewDexItems");
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction on error
                            transaction.Rollback();

                            // Log error and return a page with the error message
                            ModelState.AddModelError(string.Empty, $"An error occurred while updating the record: {ex.Message}");
                            return Page();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    ModelState.AddModelError(string.Empty, $"An error occurred while updating the record: {ex.Message}");
                    return Page();
                }
            }
            else
            {
                // Return the page with validation errors
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

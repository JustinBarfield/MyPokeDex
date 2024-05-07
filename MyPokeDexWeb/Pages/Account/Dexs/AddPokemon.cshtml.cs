using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account.Dexs
{
	[Authorize(Roles="Admin")]
    [BindProperties]
    public class AddPokemonModel : PageModel
    {
       
        public PokemonItem NewPokemon {  get; set; } = new PokemonItem();

        public List<SelectListItem> RegionID { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> Type { get; set; } = new List<SelectListItem>();

		//checkbox stuff

		public List<CategoryInfo> categories { get; set; } = new List<CategoryInfo>();
		public List<int> SelectedCategoryIDs { get; set; }

		public void OnGet()
        {
            PopulateRegionDDL();
            PopulateTypeDDL();
			PopulateCategoryList();

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
                        item.CategoryId = reader.GetInt32(0);

                        // Handle NULL values for CategoryName
                        if (!reader.IsDBNull(1))
                        {
                            item.CategoryName = reader.GetString(1);
                        }
                        else
                        {
                            // Handle the case where CategoryName is NULL
                            item.CategoryName = null; // or set a default value as desired
                        }

                        item.isSelected = false;
                        categories.Add(item);
                    }
                }
            }
        }


        public IActionResult OnPost()
        {
            // Populate the dropdown lists and category list
            PopulateCategoryList();
            PopulateRegionDDL();
            PopulateTypeDDL();

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Insert the new Pokémon item
                using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                {
                    // Insert the new Pokémon record into the Pokemon table
                    string insertPokemonQuery = @"
                INSERT INTO Pokemon ([Dex Number], Name, TypeID, [State Total], [Image URL], RegionID, Height, Weight, Audio)
                VALUES (@dexNumber, @name, @type, @stateTotal, @imageURL, @region, @height, @weight, @audio);
                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(insertPokemonQuery, conn);
                    cmd.Parameters.AddWithValue("@dexNumber", NewPokemon.DexNumber);
                    cmd.Parameters.AddWithValue("@name", NewPokemon.Name);
                    cmd.Parameters.AddWithValue("@type", NewPokemon.TypeID);
                    cmd.Parameters.AddWithValue("@stateTotal", NewPokemon.StateTotal);
                    cmd.Parameters.AddWithValue("@imageURL", NewPokemon.ImageURL);
                    cmd.Parameters.AddWithValue("@region", NewPokemon.RegionID);
                    cmd.Parameters.AddWithValue("@height", NewPokemon.Height);
                    cmd.Parameters.AddWithValue("@weight", NewPokemon.Weight);
                    cmd.Parameters.AddWithValue("@audio", NewPokemon.Audio);

                    conn.Open();
                    // Execute the insert command and get the last inserted Pokemon ID
                    int pokemonID = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insert records into the Category table for each selected category
                    foreach (int categoryID in SelectedCategoryIDs)
                    {
                        // Retrieve CategoryName based on categoryID
                        string getCategoryNameQuery = "SELECT CategoryName FROM Category WHERE CategoryID = @categoryID";
                        SqlCommand categoryNameCmd = new SqlCommand(getCategoryNameQuery, conn);
                        categoryNameCmd.Parameters.AddWithValue("@categoryID", categoryID);
                        string categoryName = categoryNameCmd.ExecuteScalar()?.ToString();

                        // Insert the record into the Category table only if categoryName is found
                        if (!string.IsNullOrEmpty(categoryName))
                        {
                            string insertCategoryQuery = "INSERT INTO Category (PokemonID, CategoryID, CategoryName) VALUES (@pokemonID, @categoryID, @categoryName)";
                            SqlCommand insertCategoryCmd = new SqlCommand(insertCategoryQuery, conn);
                            insertCategoryCmd.Parameters.AddWithValue("@pokemonID", pokemonID);
                            insertCategoryCmd.Parameters.AddWithValue("@categoryID", categoryID);
                            insertCategoryCmd.Parameters.AddWithValue("@categoryName", categoryName);

                            // Execute the insert command for each category
                            insertCategoryCmd.ExecuteNonQuery();
                        }
                    }

                    // Redirect to the ViewDexItems page
                    return RedirectToPage("ViewDexItems");
                }
            }
            else
            {
                // If the model state is not valid, return the current page
                return Page();
            }
        }



        string GetCategoryName(SqlConnection conn, int categoryID)
        {
            string categoryNameQuery = "SELECT CategoryName FROM Category WHERE CategoryID = @categoryID";
            using (SqlCommand cmd = new SqlCommand(categoryNameQuery, conn))
            {
                // Set parameters
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                // Execute the query and get the result
                object result = cmd.ExecuteScalar();

                // Return the CategoryName, or null if not found
                return result != null ? result.ToString() : null;
            }
        }



        private int GetLastInsertedPokemonID(SqlConnection conn)
        {
            // Use a query to retrieve the last inserted Pokemon ID using SCOPE_IDENTITY
            string query = "SELECT ISNULL(SCOPE_IDENTITY(), 0)";
            SqlCommand cmd = new SqlCommand(query, conn);
            object result = cmd.ExecuteScalar();

            // Convert the result to an integer and handle DBNull
            if (result == DBNull.Value)
            {
                // If the result is DBNull, return a default value (0) or handle the situation accordingly
                return 0;
            }
            else
            {
                // Convert the result to an integer
                return Convert.ToInt32(result);
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

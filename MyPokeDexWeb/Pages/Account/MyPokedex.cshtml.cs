using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;
using MyPokeDexWeb.Pages.Account.Dexs;
using System.Collections.Generic;

namespace MyPokeDexWeb.Pages.Account
{
    [BindProperties]
    public class MyPokedexModel : PageModel
    {
        // List to hold Pokémon items
        public List<PokemonItem> PokemonItems { get; set; } = new List<PokemonItem>();

        public string PokemonRegionID { get; set; }

        public int SelectedCategoryID { get; set; }

        public List<SelectListItem> CategoryList { get; set; } = new List<SelectListItem>();

        // Method to handle GET requests
        public void OnGet()
        {
            //PopulateCategories();
            PopulateAllPokemon();
        }

        // Method to handle POST requests
        public void OnPost()
        {
            // Retrieve the selected category ID from the form submission
            int selectedCategoryId = SelectedCategoryID;

            // Populate the Pokémon data based on the selected category
            PopulatePokemonByCategory(selectedCategoryId);

            // Re-populate the category list to maintain it after form submission
            //PopulateCategories();
        }

        private void PopulatePokemonByCategory(int categoryId)
        {
            // Assuming you have a database connection helper
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                // Query to retrieve all Pokémon that belong to the specified category
                string cmdText = @"
                SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total],
                       p.[image URL], p.RegionID, r.RegionName, p.Height, p.Weight, p.Audio, t.TypeName
                FROM Pokemon p
                INNER JOIN Category c ON p.PokemonID = c.PokemonId
                INNER JOIN Type t ON p.TypeID = t.TypeID
                INNER JOIN Region r ON p.RegionID = r.RegionID
                WHERE c.CategoryID = @CategoryID";

                // Create a SqlCommand object with the query and connection
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                // Add the category ID parameter to the query
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                // Open the connection
                conn.Open();

                // Execute the query and retrieve the data
                SqlDataReader reader = cmd.ExecuteReader();

                // Clear the existing list of Pokémon items
                PokemonItems.Clear();

                // Check if there are rows in the result set
                if (reader.HasRows)
                {
                    // Iterate through the result set
                    while (reader.Read())
                    {
                        // Create a new PokemonItem for each row
                        var item = new PokemonItem
                        {
                            PokemonID = reader.GetInt32(0),
                            DexNumber = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            TypeID = reader.GetInt32(3),
                            StateTotal = reader.GetInt32(4),
                            ImageURL = reader.GetString(5),
                            RegionID = reader.GetInt32(6),
                            PokemonRegionID = reader.GetString(7),
                            PokemonTypeID = reader.GetString(11),
                            Height = reader.GetString(8),
                            Weight = reader.GetString(9),
                            Audio = reader.GetString(10)
                        };

                        // Add the item to the PokemonItems list
                        PokemonItems.Add(item);
                    }
                }
            }
        }

        //private void PopulateCategories()
        //{
        //    using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
        //    {
        //        string cmdText = "SELECT CategoryID, CategoryName FROM Category";
        //        SqlCommand cmd = new SqlCommand(cmdText, conn);
        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                CategoryInfo category = new CategoryInfo();
        //                category.CategoryId = reader.GetInt32(0);

        //                // Check if the CategoryName column is NULL
        //                if (!reader.IsDBNull(1))
        //                {
        //                    category.CategoryName = reader.GetString(1);
        //                }
        //                else
        //                {
        //                    // Handle NULL case (e.g., set to default value)
        //                    category.CategoryName = string.Empty; // Set to default value (empty string)
        //                }

        //                // Add the category to your list or other data structure
        //                CategoryList.Add(category);
        //            }
        //        }
        //    }
        //}


        // Method to populate all Pokémon items
        private void PopulateAllPokemon()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total],
                                   p.[image URL], p.RegionID, r.RegionName, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
                           INNER JOIN Region r ON p.RegionID = r.RegionID
                           INNER JOIN Type t ON p.TypeID = t.TypeID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new PokemonItem
                        {
                            PokemonID = reader.GetInt32(0),
                            DexNumber = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            TypeID = reader.GetInt32(3),
                            StateTotal = reader.GetInt32(4),
                            ImageURL = reader.GetString(5),
                            RegionID = reader.GetInt32(6),
                            PokemonRegionID = reader.GetString(7),
                            PokemonTypeID = reader.GetString(11),
                            Height = reader.GetString(8),
                            Weight = reader.GetString(9),
                            Audio = reader.GetString(10)
                        };

                        PokemonItems.Add(item);
                    }
                }
            }
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;
using System.Collections.Generic;

namespace MyPokeDexWeb.Pages
{
    public class IndexModel : PageModel
    {
       
        
            // Properties for the region and type dropdown lists
            public List<SelectListItem> RegionID { get; set; } = new List<SelectListItem>();
            public int SelectedRegionID { get; set; }

            public List<SelectListItem> TypeID { get; set; } = new List<SelectListItem>();
            public int SelectedTypeID { get; set; }

            // List to hold Pokémon items
            public List<PokemonItem> PokemonItems { get; set; } = new List<PokemonItem>();

            public string PokemonRegionID { get; set; }

            // Method to handle GET requests
            public void OnGet()
            {
            PopulateAllPokemon();
        }

            // Method to handle POST requests
            public void OnPost()
            {

           

            }

        


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
                            var item = new PokemonItem();
                            item.PokemonID = reader.GetInt32(0);
                            item.DexNumber = reader.GetInt32(1);
                            item.Name = reader.GetString(2);
                            item.TypeID = reader.GetInt32(3);
                            item.StateTotal = reader.GetInt32(4);
                            item.ImageURL = reader.GetString(5);
                            item.RegionID = reader.GetInt32(6);
                            item.PokemonRegionID = reader.GetString(7); // Assign region name
                            item.PokemonTypeID = reader.GetString(11); // Assign type name here
                            item.Height = reader.GetString(8);
                            item.Weight = reader.GetString(9);
                            item.Audio = reader.GetString(10);
                            PokemonItems.Add(item);
                        }
                    }
                }
            }

          
        }




    
}

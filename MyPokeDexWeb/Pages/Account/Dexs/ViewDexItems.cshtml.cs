using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;
using System.Collections.Generic;

namespace MyPokeDexWeb.Pages.Account.Dexs
{
    [Authorize]
    [BindProperties]
    public class ViewDexItemsModel : PageModel
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
            PopulateRegionDDL();
            PopulateTypeDDL();
            
        }

		

		// Method to handle POST requests
		public void OnPost()
        {
            // Check if both RegionID and TypeID are selected
            if (SelectedRegionID != 0 && SelectedTypeID != 0)
            {
                PopulatDexItemByRegionAndType(SelectedRegionID, SelectedTypeID);
            }
            // Check if only RegionID is selected
            else if (SelectedRegionID != 0)
            {
                PopulatDexItemByRegion(SelectedRegionID);
            }
            // Check if only TypeID is selected
            else if (SelectedTypeID != 0)
            {
                PopulatDexItemByType(SelectedTypeID);
            }
            else
            {
                PopulateAllPokemon();
            }

            // Repopulate dropdown lists
            PopulateRegionDDL();
            PopulateTypeDDL();
        }


       

        // Method to populate Pokémon items based on the selected region and type
        private void PopulatDexItemByRegionAndType(int regionID, int typeID)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total],
                                   p.[image URL], p.RegionID, r.RegionName, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
                           INNER JOIN Region r ON p.RegionID = r.RegionID
                           INNER JOIN Type t ON p.TypeID = t.TypeID
                           WHERE p.RegionID = @regionID AND p.TypeID = @typeID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@regionID", regionID);
                cmd.Parameters.AddWithValue("@typeID", typeID);
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
                        item.PokemonRegionID = reader.GetString(7);
                        item.PokemonTypeName = reader.GetString(11); // Assign type name here
                        item.Height = reader.GetString(8);
                        item.Weight = reader.GetString(9);
                        item.Audio = reader.GetString(10);
                        PokemonItems.Add(item);
                    }
                }
            }
        }

        // Method to populate Pokémon items based on the selected region
        private void PopulatDexItemByRegion(int regionID)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total],
                                   p.[image URL], p.RegionID, r.RegionName, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
                           INNER JOIN Region r ON p.RegionID = r.RegionID
                           INNER JOIN Type t ON p.TypeID = t.TypeID
                           WHERE p.RegionID = @regionID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@regionID", regionID);
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
                        item.PokemonTypeName = reader.GetString(11); // Assign type name here
                        item.Height = reader.GetString(8);
                        item.Weight = reader.GetString(9);
                        item.Audio = reader.GetString(10);
                        PokemonItems.Add(item);
                    }
                }
            }
        }

        // Method to populate Pokémon items based on the selected type
        private void PopulatDexItemByType(int typeID)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total],
                                   p.[image URL], p.RegionID, r.RegionName, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
                           INNER JOIN Region r ON p.RegionID = r.RegionID
                           INNER JOIN Type t ON p.TypeID = t.TypeID
                           WHERE p.TypeID = @typeID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@typeID", typeID);
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
                        item.PokemonTypeName = reader.GetString(11); // Assign type name here
                        item.Height = reader.GetString(8);
                        item.Weight = reader.GetString(9);
                        item.Audio = reader.GetString(10);
                        PokemonItems.Add(item);
                    }
                }
            }
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
                        item.PokemonTypeName = reader.GetString(11); // Assign type name here
                        item.Height = reader.GetString(8);
                        item.Weight = reader.GetString(9);
                        item.Audio = reader.GetString(10);
                        PokemonItems.Add(item);
                    }
                }
            }
        }

        // Method to populate the region dropdown list
        private void PopulateRegionDDL()
        {
            // Populate the Region dropdown list with data from the database
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RegionID, RegionName FROM Region", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                RegionID.Add(new SelectListItem() { Text = "Select a Region", Value = "0" });

                while (reader.Read())
                {
                    RegionID.Add(new SelectListItem()
                    {
                        Text = reader.GetString(1),
                        Value = reader.GetInt32(0).ToString()
                    });
                }
            }
        }

        // Method to populate the type dropdown list
        private void PopulateTypeDDL()
        {
            // Populate the Type dropdown list with data from the database
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TypeID, TypeName FROM Type", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                TypeID.Add(new SelectListItem() { Text = "Select a Type", Value = "0" });

                while (reader.Read())
                {
                    TypeID.Add(new SelectListItem()
                    {
                        Text = reader.GetString(1),
                        Value = reader.GetInt32(0).ToString()
                    });
                }
            }
        }
    }
}

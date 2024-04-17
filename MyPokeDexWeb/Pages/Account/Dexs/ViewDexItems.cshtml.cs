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
		public int SelectedRegionID { get; set; }

		public List<SelectListItem> TypeID { get; set; } = new List<SelectListItem>();
		public int SelectedTypeID { get; set; }

		public List<PokemonItem> PokemonItems { get; set; } = new List<PokemonItem>();

        
                
        public void OnGet()
        {
            PopulateRegionDDL();
			PopulateTypeDDL();

        }

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


		private void PopulatDexItemByRegionAndType(int regionID, int typeID)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total], 
                                   p.[image URL], p.RegionID, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
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
						item.Height = reader.GetString(7);
						item.Weight = reader.GetString(8);
						item.Audio = reader.GetString(9);
						
						PokemonItems.Add(item);
					}
				}
			}
		}


		private void PopulatDexItemByRegion(int regionID)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total], 
                                   p.[image URL], p.RegionID, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
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
						item.Height = reader.GetString(7);
						item.Weight = reader.GetString(8);
						item.Audio = reader.GetString(9);
						
						PokemonItems.Add(item);
					}
				}
			}
		}


		private void PopulatDexItemByType(int typeID)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total], 
                                   p.[image URL], p.RegionID, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
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
						item.Height = reader.GetString(7);
						item.Weight = reader.GetString(8);
						item.Audio = reader.GetString(9);
						
						PokemonItems.Add(item);
					}
				}
			}
		}



		private void PopulateAllPokemon()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = @"SELECT p.PokemonID, p.[Dex Number], p.Name, p.TypeID, p.[State Total], 
                                   p.[image URL], p.RegionID, p.Height, p.Weight, p.Audio, t.TypeName
                           FROM Pokemon p
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
						item.Height = reader.GetString(7);
						item.Weight = reader.GetString(8);
						item.Audio = reader.GetString(9);
						
						PokemonItems.Add(item);
					}
				}
			}
		}


		private void PopulateTypeDDL()
        {
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "Select TypeID, TypeName From Type ORDER BY TypeName";
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
						if (type.Value == SelectedTypeID.ToString())
						{
							type.Selected = true;
						}
						TypeID.Add(type);
					}

				}
			}

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
						if(region.Value == SelectedRegionID.ToString())
						{
							region.Selected = true;
						}
						RegionID.Add(region);
					}

				}
			}
		}
	}
}

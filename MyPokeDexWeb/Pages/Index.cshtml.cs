using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


		public List<SelectListItem> RegionID { get; set; } = new List<SelectListItem>();
		public int SelectedRegionID { get; set; }

		public List<SelectListItem> TypeID { get; set; } = new List<SelectListItem>();
		public int SelectedTypeID { get; set; }

		public List<PokemonItem> PokemonItems { get; set; } = new List<PokemonItem>();

		public int PokemonRegionID { get; set; }





		public void OnGet()
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

	

		
	}
}

   
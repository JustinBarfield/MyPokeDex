using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account.Dexs
{
    [BindProperties]
    public class AddPokemonModel : PageModel
    {
       
        public Pokemon NewPokemon {  get; set; } = new Pokemon();

        public List<SelectListItem> Region { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> Type { get; set; } = new List<SelectListItem>();

		public void OnGet()
        {
            PopulateRegionDDL();

        }

		private void PopulateRegionDDL()
		{
			
		}
	}
}

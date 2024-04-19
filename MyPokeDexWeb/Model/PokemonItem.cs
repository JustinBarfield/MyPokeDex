using System.ComponentModel.DataAnnotations;
namespace MyPokeDexWeb.Model
{
    public class PokemonItem
    {
        public int PokemonID { get; set; }
        [Required]
        [Display(Name = "Pokemon Name")]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Dex Number")]
        public int DexNumber { get; set; }
        [Required]
        [Display(Name = "Type ID")]
        public int TypeID { get; set; }

		[Display(Name = "Type Name")]
		public int TypeName { get; set; }

        public string PokemonRegionID { get; set; }

        public string PokemonTypeName { get; set; }

        [Required]
        [Display(Name = "State Total")]
        public int StateTotal { get; set; }
        public string ImageURL { get; set; }
        
        [Display(Name = "Region")]
        public int RegionID { get; set; }

		[Display(Name = "Region")]
		public int RegionName { get; set; }

		public string Height { get; set; }
        public string Weight { get; set; }
        public string Audio { get; set; }

    }
}

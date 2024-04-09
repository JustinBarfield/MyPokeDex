using System.ComponentModel.DataAnnotations;
namespace MyPokeDexWeb.Model
{
    public class Pokemon
    {
        public int PokemonID { get; set; }
        [Required]
        [Display(Name = "Pokemon Name")]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Dex Number")]
        public int DexNumber { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "State Total")]
        public int StateTotal { get; set; }
        public string ImageURL { get; set; }
        [Required]
        [Display(Name = "Region")]
        public string Region { get; set; }

        public string Height { get; set; }
        public string Weight { get; set; }
        public string Audio { get; set; }

    }
}

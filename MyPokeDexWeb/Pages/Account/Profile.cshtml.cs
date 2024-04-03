using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPokeDexWeb.Model;

namespace MyPokeDexWeb.Pages.Account
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public UserProfile profile { get; set; }
        public void OnGet()
        {
        }
    }
}

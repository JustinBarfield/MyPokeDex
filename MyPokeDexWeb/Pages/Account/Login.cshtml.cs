using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyPokeDexWeb.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string LoginUser { get; set; }
        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("Profile");
            }
            else
            {
                return Page();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace CoinTrading.Pages
{
    public class SignupModel : PageModel
    {
        public void OnGet()
        {
            Debug.WriteLine("Test");
        }

        public void OnPost()
        {
            /*if (!ModelState.IsValid)
            {
                //return Page();
            }*/

            Debug.WriteLine("Test i PostAsync");

            //return RedirectToPage("./Index");
        }
    }
}

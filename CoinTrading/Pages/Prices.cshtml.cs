using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication_Sqlite_Test_Slask.Pages
{
    public class PricesModel : PageModel
    {
        public JsonResult OnGet()
        {
            Random rnd = new Random();
            return new JsonResult(new { Close = rnd.Next(55000, 90000) });
        }

        public JsonResult OnPost()
        {
            Random rnd = new Random();
            return new JsonResult(new { Close = rnd.Next(55000, 90000) });
        }
    }
}

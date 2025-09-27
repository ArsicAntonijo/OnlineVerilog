using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineVerilog.Pages
{
    public class HealthcheckModel : PageModel
    {
        public IActionResult OnGet()
        {
            return new JsonResult(new { status = "Healthy" });
        }
    }
}

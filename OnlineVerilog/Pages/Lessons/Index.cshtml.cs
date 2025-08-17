using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineVerilog.Pages.Lessons
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        public void OnGet(int? id)
        {
            Id = id ?? 0;
        }
    }
}

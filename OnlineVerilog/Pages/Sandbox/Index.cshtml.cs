using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineVerilog.Service;

namespace OnlineVerilog.Pages.Sandbox
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly VerilogHelper _vh;
        //[BindProperty]
        //public string FileName { get; set; }
        [BindProperty]
        public string Code { get; set; }
        [BindProperty]
        public string TestbenchCode { get; set; }
        public IndexModel(ILogger<IndexModel> logger, VerilogHelper vh)
        {
            _logger = logger;
            _vh = vh;
        }

        public void OnGet()
        {
            ViewData["Output"] = "";
        }
        public void OnPost()
        {
            ViewData["Output"] = _vh.ExecuteTheProcess("topmodule", Code);
        }
    }
}

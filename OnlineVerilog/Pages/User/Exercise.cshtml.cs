using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineVerilog.Service;

namespace OnlineVerilog.Pages.User
{
    public class ExerciseModel : PageModel
    {
        private readonly ILogger<ExerciseModel> _logger;
        private readonly VerilogHelper _vh;
        [BindProperty]
        public string FileName { get; set; }
        [BindProperty]
        public string Code { get; set; }
        public ExerciseModel(ILogger<ExerciseModel> logger, VerilogHelper vh)
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
            ViewData["Output"] = _vh.ExecuteTheProcess(FileName, Code);
        }
    }
}

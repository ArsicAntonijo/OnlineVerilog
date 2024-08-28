using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVerilog.Context;
using OnlineVerilog.Models;
using OnlineVerilog.Service;

namespace OnlineVerilog.Pages.ExamplesSection
{
    public class IndexModel : PageModel
    {
        private readonly OnlineVerilog.Context.VeronContext _context;
        private readonly OnlineVerilog.Service.VerilogHelper _vh;
        private readonly UserManager<User> _userManager;
        private readonly string initialSolution = "module topmodule;\r\rendmodule\r";

        public IndexModel(OnlineVerilog.Context.VeronContext context, Service.VerilogHelper vh, UserManager<User> um)
        {
            _context = context;
            _vh = vh;
            _userManager = um;
        }

        public IActionResult OnGet(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int Id = int.Parse(id);
                Example = _context.Examples.Where(e => e.Id == Id).FirstOrDefault();
                if (Example != null)
                {
                    ViewData["Header"] = Example.Header;
                    ViewData["Section"] = Example.Section;
                    ViewData["Body"] = Converting.StringToHtml(Example.Body);
                    ViewData["Testbench"] = Example.TestBench;
                    ViewData["ImagePath"] = Example.imagePath;
                }
                
            }
            return Page();
        }
        [BindProperty]
        public Example Example { get; set; } = default!;
        [BindProperty]
        public string Solution { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string output = VerilogHelper.ValidateSolution(Solution);
            ViewData["Output"] = output;
            if (string.IsNullOrEmpty(output))
            {
                (string runoutput, string dumpfilepath, bool status) = _vh.ExecuteTheProcess("topmodule.v", Solution, "testbench.v", Example.TestBench);

                ViewData["Output"] = Converting.StringToHtml(runoutput);
                if (!string.IsNullOrEmpty(dumpfilepath))
                {
                    ViewData["DumpFilePath"] = $"<a href=\"{dumpfilepath}\"]\" target=\"_blank\">Кликните овде, да бисте видели визуелно промену вредности коришћених сигнала.</a>";
                }
                if (status && User.Identity.IsAuthenticated)
                {
                    string userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
                    _context.SolvedExamples.Add(new SolvedExample() { ExampleId = Example.Id, UserId = userId });
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}

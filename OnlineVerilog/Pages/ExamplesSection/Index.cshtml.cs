using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.Pages.ExamplesSection
{
    public class IndexModel : PageModel
    {
        private readonly OnlineVerilog.Context.VeronContext _context;

        public IndexModel(OnlineVerilog.Context.VeronContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int Id = int.Parse(id);
                Example = _context.Examples.Where(e => e.Id == Id).FirstOrDefault();
                ViewData["Header"] = Example.Header;
                ViewData["Section"] = Example.Section;
                ViewData["Body"] = Example.Body;
            }
            return Page();
        }

        public Example Example { get; set; } = default!;
        [BindProperty]
        public string Solution { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Examples.Add(Example);
            //await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}

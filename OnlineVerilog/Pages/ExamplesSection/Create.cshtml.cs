using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.Pages.ExamplesSection
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly OnlineVerilog.Context.VeronContext _context;

        public CreateModel(OnlineVerilog.Context.VeronContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Example Example { get; set; } = default!;
        [BindProperty]
        public IFormFile? Upload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", Upload.FileName);
                if (!Directory.Exists(Path.GetDirectoryName(file))) Directory.CreateDirectory(Path.GetDirectoryName(file));
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                Example.imagePath = "/uploads/"+ Upload.FileName;
            }
            catch(Exception ex) { }
            _context.Examples.Add(Example);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}

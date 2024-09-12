using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.Pages.ExamplesSection
{
    public class EditModel : PageModel
    {
        private readonly OnlineVerilog.Context.VeronContext _context;

        public EditModel(OnlineVerilog.Context.VeronContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Example Example { get; set; } = default!;
        [BindProperty]
        public IFormFile? Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var example =  await _context.Examples.FirstOrDefaultAsync(m => m.Id == id);
            if (example == null)
            {
                return NotFound();
            }
            Example = example;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Upload != null)
            {
                try
                {
                    var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", Upload.FileName);
                    if (!Directory.Exists(Path.GetDirectoryName(file))) Directory.CreateDirectory(Path.GetDirectoryName(file));
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }
                    Example.imagePath = "/uploads/" + Upload.FileName;
                }
                catch (Exception) { }
            }

            _context.Attach(Example).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExampleExists(Example.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./List");
        }

        private bool ExampleExists(int id)
        {
            return _context.Examples.Any(e => e.Id == id);
        }
    }
}

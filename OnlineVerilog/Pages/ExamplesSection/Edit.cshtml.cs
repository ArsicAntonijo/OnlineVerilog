using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
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
        private readonly IVeronRepository _repo;

        public EditModel(IVeronRepository vr)
        {
            _repo = vr;
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

            var example = _repo.GetExample(id);  
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

            if(!(await _repo.UpdateExample(Example)))
            {
                return NotFound();
            }      

            return RedirectToPage("./List");
        }

        private bool ExampleExists(int id)
        {
            return _repo.ExampleExists(id);
        }
    }
}

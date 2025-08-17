using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
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
        private readonly IVeronRepository _repo;

        public CreateModel(IVeronRepository vr)
        {
            _repo = vr;
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
                if (Upload != null)
                {
                var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", Upload.FileName);
                if (!Directory.Exists(Path.GetDirectoryName(file))) Directory.CreateDirectory(Path.GetDirectoryName(file));
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                    Example.imagePath = "/uploads/" + Upload.FileName;
                }                
            }
            catch(Exception ex) { }
            _repo.AddNewExample(Example);            

            return RedirectToPage("../Index");
        }
    }
}

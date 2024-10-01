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
    public class DeleteModel : PageModel
    {
        private readonly IVeronRepository _repo;

        public DeleteModel(IVeronRepository vr)
        {
            _repo = vr;
        }

        [BindProperty]
        public int? Id { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Id = id;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _repo.DeleteExample(Id);             
            return RedirectToPage("./List");
        }

        private bool ExampleExists(int id)
        {
            return _repo.ExampleExists(id); 
        }
    }
}

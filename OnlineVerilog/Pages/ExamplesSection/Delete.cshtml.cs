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
    public class DeleteModel : PageModel
    {
        private readonly OnlineVerilog.Context.VeronContext _context;

        public DeleteModel(OnlineVerilog.Context.VeronContext context)
        {
            _context = context;
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
            try
            {
                //var solvedExamples = _context.SolvedExamples.Where(se => se.ExampleId == Id).ToList();

                //_context.SolvedExamples.RemoveRange(solvedExamples);

                var example = _context.Examples.Where(e => e.Id == Id).Include(se => se.SolvedByUsers).FirstOrDefault();

                if (example != null) 
                {
                    _context.Remove(example);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            


            return RedirectToPage("./List");
        }

        private bool ExampleExists(int id)
        {
            return _context.Examples.Any(e => e.Id == id);
        }
    }
}

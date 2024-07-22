using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.Pages.ExamplesSection
{
    public class ListModel : PageModel
    {
        private readonly OnlineVerilog.Context.VeronContext _context;

        public ListModel(OnlineVerilog.Context.VeronContext context)
        {
            _context = context;
        }

        public IList<Example> Example { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Example = await _context.Examples.ToListAsync();
        }
    }
}

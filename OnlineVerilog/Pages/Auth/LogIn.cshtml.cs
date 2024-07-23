using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.Pages.Auth
{
    public class LogInModel : PageModel
    {
        private readonly OnlineVerilog.Context.VeronContext _context;

        public LogInModel(OnlineVerilog.Context.VeronContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            User? u = _context.Users.Where(x => x.Email == User.Email).FirstOrDefault();
            if (u != null && u.Password.Equals(User.Password))
            {
                // Ovde sesiju kreiramo
                //int type = u.Type.Equals("admin") ? 2 : 1;
                //HttpContext.Session.SetInt32("Active", type);
                //HttpContext.Session.SetString("Name", u.Name);
                return RedirectToPage("../Index");
            }
            return Page();
        }
    }
}

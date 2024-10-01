using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Context;

namespace OnlineVerilog.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IVeronRepository _repo;

        public DashboardModel(IVeronRepository rep)
        {
            _repo = rep;
        }

        public IList<ModifiedExample> ModifiedExamples { get; set; } = default!;
        public void OnGet()
        {
            ModifiedExamples = _repo.GetModifiedExamples();
        }        
    }
}

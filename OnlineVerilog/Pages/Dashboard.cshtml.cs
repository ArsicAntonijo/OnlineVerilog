using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Context;

namespace OnlineVerilog.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly VeronContext _context;

        public DashboardModel(VeronContext context)
        {
            _context = context;
        }

        public IList<ModifiedExample> ModifiedExamples { get; set; } = default!;
        public void OnGet()
        {
            int total = _context.Examples.ToList().Count;
            //var se = _context.SolvedExamples.Include(se => se.SolvedByUser).ToList();
            //ModifiedExamples = se.GroupBy(se => se.SolvedByUser).Select(me => new ModifiedExample 
            //    {
            //        Name = me.Key.FirstName,
            //        TotalExamples = total,
            //        SolvedExamples = me.Select(e => e.ExampleId).Distinct().Count()
            //}).ToList();
            var users = _context.Users.Include(u => u.SolvedExamples).ToList();
            ModifiedExamples = users.Select(me => new ModifiedExample
            {
                Name = me.FirstName,
                TotalExamples = total,
                SolvedExamples = me.SolvedExamples.Count
            }).ToList();
        }

        public class ModifiedExample
        {
            public string Name { get; set; }
            public int SolvedExamples { get; set; }
            public int TotalExamples { get; set;}
        }
    }
}

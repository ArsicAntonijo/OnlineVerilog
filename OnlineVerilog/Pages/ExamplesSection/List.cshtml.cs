using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.Pages.ExamplesSection
{
    public class ListModel : PageModel
    {
        private readonly IVeronRepository _repo;

        public ListModel(IVeronRepository repo)
        {
            _repo = repo;
        }

        public IList<Example> Example { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Example = _repo.GetExamples(); 
        }
    }
}

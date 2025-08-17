using Data.Repositories;
using Microsoft.AspNetCore.Identity;
﻿using Microsoft.AspNetCore.Mvc;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.ViewComponents
{
    public class ExamplesViewComponent : ViewComponent
    {
        private IVeronRepository _repo;
        private readonly UserManager<User> _userManager;
        public ExamplesViewComponent(IVeronRepository vr, UserManager<User> um) { _repo = vr; _userManager = um; }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Example> examples;
            if (User.Identity.IsAuthenticated)
            {
                string userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
                examples = _repo.GetExamples();
                var solvedExamples = _repo.GetSolvedExamples(userId); 
                foreach (var e in examples)
                {
                    if (solvedExamples.Any(se => se.ExampleId == e.Id))
                    {
                        e.SolvedByUsers = new List<SolvedExample>();
                    }
                }
            }
            else
            {
                examples = _repo.GetExamples();
            }
           // var examples = _context.Examples.ToList();
            return View(examples);
        }
    }
}

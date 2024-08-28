using Microsoft.AspNetCore.Identity;
﻿using Microsoft.AspNetCore.Mvc;
using OnlineVerilog.Context;
using OnlineVerilog.Models;

namespace OnlineVerilog.ViewComponents
{
    public class ExamplesViewComponent : ViewComponent
    {
        private VeronContext _context;
        private readonly UserManager<User> _userManager;
        public ExamplesViewComponent(VeronContext vr, UserManager<User> um) { _context = vr; _userManager = um; }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Example> examples;
            if (User.Identity.IsAuthenticated)
            {
                string userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
                examples = _context.Examples.ToList();
                var solvedExamples = _context.SolvedExamples.Where(se => se.UserId == userId).ToList();
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
                examples = _context.Examples.ToList();
            }
           // var examples = _context.Examples.ToList();
            return View(examples);
        }
    }
}

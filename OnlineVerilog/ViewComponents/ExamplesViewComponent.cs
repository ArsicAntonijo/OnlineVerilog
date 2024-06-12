using Microsoft.AspNetCore.Mvc;
using OnlineVerilog.Context;

namespace OnlineVerilog.ViewComponents
{
    public class ExamplesViewComponent : ViewComponent
    {
        private VeronContext _context;
        public ExamplesViewComponent(VeronContext vr) { _context = vr; }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var examples = _context.Examples.ToList();
            return View(examples);
        }
    }
}

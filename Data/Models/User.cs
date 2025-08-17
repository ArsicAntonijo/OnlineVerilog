using Microsoft.AspNetCore.Identity;

namespace OnlineVerilog.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public ICollection<SolvedExample> SolvedExamples { get; set; }// = new List<Example>();
    }
}

using Microsoft.AspNetCore.Identity;

namespace OnlineVerilog.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
    }
}

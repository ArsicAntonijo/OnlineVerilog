namespace OnlineVerilog.Models
{
    public class Example
    {
        public int Id { get; set; }
        public string Section { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string TestBench { get; set; }
        public string? imagePath { get; set; } = string.Empty;
        public ICollection<SolvedExample>? SolvedByUsers { get; set; } //= new List<User>();
    }
}

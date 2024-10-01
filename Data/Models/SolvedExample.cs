namespace OnlineVerilog.Models
{
    public class SolvedExample
    {
        public int ExampleId { get; set; }
        public Example Example { get; set; }
        public string UserId {  get; set; }
        public User SolvedByUser { get; set; }
    }
}

namespace Redemption.Models.Data
{
    public class Log
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Rps1 { get; set; }
        public bool Rps2 { get; set; }
        public bool Rps3 { get; set; }
    }
}

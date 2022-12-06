namespace Redemption.Models.Data
{
    public class Log
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? TransactionId { get; set; }
        
        public string? ResultCode { get; set; }
        public string? MessageCode { get; set; }
        public string? ExternalId { get; set; }
        public string? Message { get; set; }
    }
}

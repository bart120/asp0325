namespace DocaSub.Models
{
    public class SubRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public decimal Amount { get; set; } = 0;
        public int Status { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
    }
}

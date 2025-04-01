using System.ComponentModel.DataAnnotations.Schema;

namespace DocaSub.Models
{
    [Table("sub_requests")]
    public class SubRequest
    {
        public int Id { get; set; }

        [Column("titre")]
        public string Title { get; set; } = "";
        public decimal Amount { get; set; } = 0;
        public int Status { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public int Priority { get; set; }
    }
}

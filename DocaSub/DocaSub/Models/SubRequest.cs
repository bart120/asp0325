using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocaSub.Models
{
    [Table("sub_requests")]
    public class SubRequest
    {
        public int Id { get; set; }

        [Column("titre")]
        [Display(Name = "Nom de la demande", Prompt = "Votre demande")]
        [Required(ErrorMessage = "{0} obligatoire")]
        public string Title { get; set; } = "";

        [Display(Name = "Montant")]
        [Required(ErrorMessage = "{0} obligatoire")]
        public decimal Amount { get; set; } = 0;
        public int Status { get; set; } = 0;
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public int Priority { get; set; }

        
        public int SubventionId { get; set; }
        [ForeignKey("SubventionId")]
        public virtual Subvention Subvention { get; set; }
    }
}

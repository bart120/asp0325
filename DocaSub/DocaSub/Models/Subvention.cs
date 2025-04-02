using System.ComponentModel.DataAnnotations;

namespace DocaSub.Models
{
    public class Subvention
    {
        //[Key]
        public int Id { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "{0} est obligatoire")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} doit être compris entre {2} et {1} caractères")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "{0} est obligatoire")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "{0} est obligatoire")]
        [Display(Name = "Partenaire")]
        public string Partner { get; set; } = "";

        [Display(Name = "Type")]
        [Required(ErrorMessage = "{0} est obligatoire")]
        public int Category { get; set; } = 0;

        [Display(Name = "Début de validité")]
        [Required(ErrorMessage = "{0} est obligatoire")]
        [DataType(DataType.Date)]
        //[RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "Le format de la date est invalide")]
        public DateTime Start { get; set; }

        [Display(Name = "Fin de validité")]
        [DataType(DataType.Date)]
        //[Compare("Start", ErrorMessage = "La date de fin doit être supérieure à la date de début")]
        public DateTime? End { get; set; }

        public virtual ICollection<SubRequest> SubRequests { get; set; }
    }
}

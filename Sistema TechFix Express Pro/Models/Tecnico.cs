using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_TechFix_Express_Pro.Models
{
    [Table("Tecnicos")]
    public class Tecnico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo del técnico es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Técnico Responsable")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        [StringLength(50, ErrorMessage = "La especialidad no puede superar los 50 caracteres.")]
        public string Especialidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string Telefono { get; set; } = string.Empty;

        public ICollection<OrdenServicio>? Ordenes { get; set; }
    }
}
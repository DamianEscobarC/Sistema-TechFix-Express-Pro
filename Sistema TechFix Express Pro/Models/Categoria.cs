using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_TechFix_Express_Pro.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        [Display(Name = "Categoría")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(150, ErrorMessage = "La descripción no puede superar los 150 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<OrdenServicio>? Ordenes { get; set; }
    }
}
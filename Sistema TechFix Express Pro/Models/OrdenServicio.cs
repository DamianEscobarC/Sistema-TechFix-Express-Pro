using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sistema_TechFix_Express_Pro.Models;

namespace Sistema_TechFix_Express_Pro.Models
{
    [Table("OrdenesServicio")]
    public class OrdenServicio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de orden es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de orden no puede exceder los 20 caracteres.")]
        [Display(Name = "N° de Orden")]
        public string NumeroOrden { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre del cliente debe tener entre 3 y 100 caracteres.")]
        [Display(Name = "Nombre del Cliente")]
        public string NombreCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono de contacto es obligatorio.")]
        [Phone(ErrorMessage = "Ingrese un formato telefónico válido.")]
        [StringLength(15, ErrorMessage = "El teléfono no puede superar los 15 caracteres.")]
        [Display(Name = "Teléfono de Contacto")]
        public string TelefonoCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar una categoría de dispositivo.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría válida existente.")]
        [Display(Name = "Categoría de Dispositivo")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        [Required(ErrorMessage = "Debe asignar un técnico responsable.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un técnico responsable válido.")]
        [Display(Name = "Técnico Asignado")]
        public int TecnicoId { get; set; }

        [ForeignKey("TecnicoId")]
        public Tecnico? Tecnico { get; set; }

        [Required(ErrorMessage = "La descripción de la falla es obligatoria.")]
        [StringLength(250, ErrorMessage = "La falla reportada no puede superar los 250 caracteres.")]
        [Display(Name = "Falla Reportada")]
        public string DescripcionFalla { get; set; } = string.Empty;

        [Required(ErrorMessage = "El presupuesto estimado es obligatorio.")]
        [Range(5000, 500000, ErrorMessage = "El presupuesto debe estar entre $5.000 y $500.000 pesos.")]
        [Display(Name = "Presupuesto Estimado ($)")]
        public double CostoEstimado { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El estado de la orden es obligatorio.")]
        [RegularExpression("^(Pendiente|En Reparacion|Listo|Entregado)$", ErrorMessage = "Estado no válido. Valores admitidos: Pendiente, En Reparacion, Listo, Entregado.")]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";

        [Display(Name = "¿Aplica Garantía Técnica?")]
        public bool TieneGarantia { get; set; }
    }
}
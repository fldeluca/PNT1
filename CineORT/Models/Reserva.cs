using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineORT.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }
        [Display(Name = "Fecha de Reserva")]
        public DateTime? FechaReserva { get; set; }
        public double? Precio { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Range(1, 100)]
        public int CantidadAsientos { get; set; }
        [Display(Name = "Reserva confirmada")]
        public bool? ReservaConfirmada { get; set; } = false;
        public int? ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        public int? FuncionId { get; set; }
        [ForeignKey("FuncionId")]
        public Funcion? Funcion { get; set; }
    }
}

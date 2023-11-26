using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineORT.Models
{
    public class Funcion
    {
        [Key]
        public int FuncionId { get; set; }
        [Display(Name = "Horario de la función")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public DateTime HorarioFuncion { get; set; }
        public int? AsientosDisponibles { get; set; }
        public bool? IsLlena { get; set; }
        //Relación con Reserva 
        public int? ReservaId { get; set; }
        [ForeignKey("ReservaId")]
        public Reserva? Reserva { get; set; }

        //Relaciones con Sala Y Película
        public Sala? Sala { get; set; }
        public Pelicula? Pelicula { get; set; }
    }
}

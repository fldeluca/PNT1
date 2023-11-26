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

        //Relaciones con Sala Y Película
        public int? SalaId { get; set; }
        [ForeignKey("SalaId")]
        public Sala? Sala { get; set; }
        public int? PeliculaId { get; set; }
        [ForeignKey("PeliculaId")]

        public Pelicula? Pelicula { get; set; }
    }
}

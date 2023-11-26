using System.ComponentModel.DataAnnotations;

namespace CineORT.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(2, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Range(30,150)]
        public int Capacidad { get; set; }

        //Relación con Función 1 a 1
        public int? FuncionId { get; set; }
        public Funcion? Funcion { get; set; }
    }
}

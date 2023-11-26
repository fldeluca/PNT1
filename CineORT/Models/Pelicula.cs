using System.ComponentModel.DataAnnotations;

namespace CineORT.Models
{
    public class Pelicula
    {
        [Key]
        public int PeliculaId { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(2, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Display(Name = "Duración en minutos")]
        public int Duracion { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(2, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(200, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Sinopsis { get; set; }
        public string? Imagen { get; set; }

        //Relación con Género
        public Genero Genero { get; set; }
        //Relación con Clasificación
        public Clasificacion Clasificacion { get; set; }
    }
}

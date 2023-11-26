using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineORT.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(2, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }
    }
}

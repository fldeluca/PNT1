using System.ComponentModel.DataAnnotations;

namespace CineORT.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(2, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]

        public string Nombre { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(2, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(10, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(30, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Email { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(8, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(8, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Dni { get; set; }
        List<Reserva>? Reservas { get; set; }
    }

}

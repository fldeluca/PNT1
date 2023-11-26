namespace CineORT.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        //Errores personalizados para las vistas
        public const string CampoRequerido = "{0} es obligatorio.";
        public const string CaracteresMinimos = "{0} no debe ser inferior a los {1} caracteres.";
        public const string CaracteresMaximos = "{0} no debe superar los {1} caracteres.";
        public const string PrecioValido = "{0} debe ser mayor a $ {1} y menor a {2}.";

    }
}
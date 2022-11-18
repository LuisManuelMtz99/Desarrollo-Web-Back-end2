using System.ComponentModel.DataAnnotations;
using JuegosApi.Validaciones;
namespace JuegosApi.DTOs
{
    public class DatoCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimerLetraMayuscula]
        public string Genero { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<int> JuegosIds { get; set; }
    }
}

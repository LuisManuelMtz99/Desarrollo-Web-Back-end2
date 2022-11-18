using System.ComponentModel.DataAnnotations;
using JuegosApi.Validaciones;
namespace JuegosApi.DTOs
{
    public class DatoPatchDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimerLetraMayuscula]
        public string Name { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

}

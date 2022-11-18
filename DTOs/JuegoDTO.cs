using JuegosApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace JuegosApi.DTOs
{
    public class JuegoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimerLetraMayuscula]
        public string Name { get; set; }
    }
}


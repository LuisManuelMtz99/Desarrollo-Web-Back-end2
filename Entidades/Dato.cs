using System.ComponentModel.DataAnnotations;
using JuegosApi.Validaciones;
using Microsoft.AspNetCore.Identity;

namespace JuegosApi.Entidades

{
    public class Dato
    {

        public int Id { get; set; }
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} solo puede tener hasta 20 caracteres.")]
        [PrimerLetraMayuscula]
        public string Genero { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }

        public List<JuegoDato> JuegoDato { get; set; }
    }
}

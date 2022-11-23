using Microsoft.AspNetCore.Identity;

namespace JuegosApi.Entidades
{
    public class Secuelas
    {
        public int Id { get; set; }
        public string Secuela { get; set; }

        public int SecuelaId { get; set; }

        public Dato Dato { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
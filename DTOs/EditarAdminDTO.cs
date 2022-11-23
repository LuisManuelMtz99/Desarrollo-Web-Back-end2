using System.ComponentModel.DataAnnotations;

namespace JuegosApi.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
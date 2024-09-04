using FormularioContactoAPI.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormularioContactoAPI.Models
{
    public class FormularioContacto
    {
        public int Id { get; set; }
        [Required]
        [ProhibitDomains("example.com", "prohibited.com")]
        public string Email { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Comentarios { get; set; }
        [NotMapped]
        public IFormFile? Adjunto { get; set; }
    }
}

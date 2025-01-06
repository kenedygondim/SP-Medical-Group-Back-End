using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpMedicalGroup.Dto.FotoPerfil
{
    public record AlterarFotoPerfilDto
    {
        [Required]
        public required string Email ;

        [Required]
        public required IFormFile FotoPerfil;
    }
}
    
using System.ComponentModel.DataAnnotations;

namespace SpMedicalGroup.Dto.Usuario
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário!")]
        public required string Senha { get; set; }
    }
}

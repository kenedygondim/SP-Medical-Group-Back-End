using System.ComponentModel.DataAnnotations;

namespace SpMedicalGroup.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário!")]
        public required string Senha { get; set; }
    }
}

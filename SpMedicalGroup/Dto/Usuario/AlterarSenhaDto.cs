using System.Globalization;

namespace SpMedicalGroup.Dto.Usuario
{
    public record AlterarSenhaDto
    {
        public required string SenhaAtual { get; set; }
        public required string NovaSenha { get; set; }
        public required string EmailUsuario { get; set; }
    }
}

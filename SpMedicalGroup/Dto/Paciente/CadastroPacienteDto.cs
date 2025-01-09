using System.ComponentModel.DataAnnotations.Schema;

namespace SpMedicalGroup.Dto.Paciente
{
    public record CadastroPacienteDto
    {
        public required string NomeCompleto { get; set; }
        public required string DataNascimento { get; set; } = string.Empty;
        public required string Rg { get; set; } = string.Empty;
        public required string Cpf { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Senha { get; set; } = string.Empty;
        public required string Cep { get; set; } = string.Empty;
        public required string Logradouro { get; set; } = string.Empty;
        public required string Numero { get; set; } = string.Empty;
        public required string Bairro { get; set; } = string.Empty;
        public required string Municipio { get; set; } = string.Empty;
        public required string Uf { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? FotoPerfilFile { get; set; }
    }
}

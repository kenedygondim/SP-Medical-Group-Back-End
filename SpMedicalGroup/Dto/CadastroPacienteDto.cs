using SpMedicalGroup.Domains;

namespace SpMedicalGroup.Dto
{
    public class CadastroPacienteDto
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string DataNascimento { get; set; } = string.Empty;
        public string Rg { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }
}

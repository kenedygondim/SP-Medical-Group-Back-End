namespace SpMedicalGroup.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public required string Cep { get; set; }
        public required string Uf { get; set; }
        public required string Municipio { get; set; }
        public required string Bairro { get; set; }
        public required string Logradouro { get; set; }
        public required string Numero { get; set; }
        public string? Complemento { get; set; }
    }
}

namespace SpMedicalGroup.Dto
{
    public class ConfirmarConsultaDetalhesDto
    {
        public decimal valorConsulta { get; set; }
        public string nomeEmpresa { get; set; } = string.Empty;
        public string municipio { get; set; } = string.Empty;
        public string bairro { get; set; } = string.Empty;
        public string logradouro { get; set; } = string.Empty;
        public string numero { get; set; } = string.Empty;
        public string complemento { get; set; } = string.Empty;
    }
}

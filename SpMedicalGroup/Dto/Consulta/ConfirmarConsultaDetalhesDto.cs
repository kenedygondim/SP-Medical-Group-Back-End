namespace SpMedicalGroup.Dto.Consulta
{
    public class ConfirmarConsultaDetalhesDto
    {
        public decimal ValorConsulta { get; set; }
        public string NomeEmpresa { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }
}

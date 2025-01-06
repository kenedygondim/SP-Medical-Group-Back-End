namespace SpMedicalGroup.Dto.Consulta
{
    public record ConsultaDetalhadaDto
    {
        public required int ConsultaId { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public string CpfPaciente { get; set; } = string.Empty;
        public string NomeMedico { get; set; } = string.Empty;
        public string fotoPerfilUrl { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string DataConsulta { get; set; } = string.Empty;
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFim { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public bool IsTelemedicina { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }

}

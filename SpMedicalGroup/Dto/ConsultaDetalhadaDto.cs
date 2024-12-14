namespace SpMedicalGroup.Dto
{
    public class ConsultaDetalhadaDto
    {
        public string NomePaciente { get; set; } = string.Empty;
        public string CpfPaciente { get; set; } = string.Empty;
        public string NomeMedico { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string DataConsulta { get; set; } = string.Empty;
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFim { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }

}

namespace SpMedicalGroup.Dto.Consulta
{
    public class AgendarConsultaDto
    {
        public required int DisponibilidadeId { get; set; }
        public required int EspecialidadeId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string EmailPaciente { get; set; } = string.Empty;
        public bool IsTelemedicina { get; set; }
    }
}

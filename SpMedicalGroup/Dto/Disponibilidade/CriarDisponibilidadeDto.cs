namespace SpMedicalGroup.Dto.Disponibilidade
{
    public record CriarDisponibilidadeDto
    {
        public required string EmailMedico { get; set; }
        public required string DataDisp { get; set; }
        public required string HoraInicio { get; set; }
        public required string HoraFim { get; set; }
    }
}

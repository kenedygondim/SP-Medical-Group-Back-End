namespace SpMedicalGroup.Dto
{
    public class CriarDisponibilidadeDto
    {
        public required string emailMedico { get; set; }
        public required string DataDisp { get; set; }
        public required string HoraInicio { get; set; }
        public required string HoraFim { get; set; }
    }
}

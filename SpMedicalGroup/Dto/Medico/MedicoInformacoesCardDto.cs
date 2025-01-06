using static System.Net.Mime.MediaTypeNames;

namespace SpMedicalGroup.Dto.Medico
{
    public record MedicoInformacoesCardDto
    {
        public string Cpf { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public string FotoPerfilUrl { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
    }
}
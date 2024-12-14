using static System.Net.Mime.MediaTypeNames;

namespace SpMedicalGroup.Dto
{
    public class MedicoInformacoesCardDto
    {
        public string cpf { get; set; } = string.Empty;
        public string nomeCompleto { get; set; } = string.Empty;
        public string crm { get; set; } = string.Empty;
        public string fotoPerfilUrl { get; set; } = string.Empty;
        public string nomeFantasia { get; set; } = string.Empty;
    }
}
namespace SpMedicalGroup.Dto.Medico
{
    public record InformacoesMedicoPopUp
    {
        public string Cpf { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public string FotoPerfilUrl { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DataNascimento { get; set; } = string.Empty;
        public int NumeroConsultas { get; set; }
    }
}

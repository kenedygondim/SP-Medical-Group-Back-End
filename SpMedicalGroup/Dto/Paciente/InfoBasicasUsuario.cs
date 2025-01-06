namespace SpMedicalGroup.Dto.Paciente
{
    public record InfoBasicasUsuario
    {
        public required string Cpf { get; set; }
        public required string NomeCompleto { get; set; }
        public required string FotoPerfilUrl { get; set; }
    }
}

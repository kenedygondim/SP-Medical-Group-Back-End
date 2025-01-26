namespace SpMedicalGroup.Dto.Especialidade
{
    public record AdicionarEspecialidadeDto
    {
        public required string NomeEspecialidade { get; set; }
        public required string DescricaoEspecialidade { get; set; }
    }
}

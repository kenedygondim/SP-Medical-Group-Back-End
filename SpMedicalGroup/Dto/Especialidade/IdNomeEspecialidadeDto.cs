namespace SpMedicalGroup.Dto.Especialidade
{
    public record IdNomeEspecialidadeDto
    {
        public int EspecialidadeId { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}

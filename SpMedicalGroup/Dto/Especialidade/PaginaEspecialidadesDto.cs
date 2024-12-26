namespace SpMedicalGroup.Dto.Especialidade
{
    public class PaginaEspecialidadesDto
    {
        public string Especialidade { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int NumeroMedicos { get; set; }
        public decimal PrecoMinimo { get; set; }

    }
}



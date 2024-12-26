namespace SpMedicalGroup.Models
{
    public class MedicoEspecialidade
    {
        public required string CpfMedico { get; set; }

        public virtual Medico Medico { get; set; }
        public required int EspecialidadeId { get; set; }
        public virtual Especialidade Especialidade { get; set; }
        public required decimal ValorProcedimento { get; set; }
    }
}
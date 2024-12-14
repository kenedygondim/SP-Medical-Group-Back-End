using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Principal;

namespace SpMedicalGroup.Domains
{
    public class Especialidade
    {
        public int EspecialidadeId { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public virtual ICollection<MedicoEspecialidade> MedicoEspecialidade { get; set; }

    }
}
using System.Text.Json.Serialization;

namespace SpMedicalGroup.Models
{
    public class Especialidade
    {
        public int EspecialidadeId { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        [JsonIgnore]
        public virtual ICollection<MedicoEspecialidade> MedicoEspecialidade { get; set; }

    }
}
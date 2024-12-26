using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SpMedicalGroup.Models
{
    public class Disponibilidade
    {
        public int DisponibilidadeId { get; set; }
        public required string CpfMedico { get; set; }
        public required string DataDisp { get; set; }
        public required string HoraInicio { get; set; }
        public required string HoraFim { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual Medico? Medico { get; set; }
    }
}


using System.Text.Json.Serialization;

namespace SpMedicalGroup.Models
{
    public class Empresa
    {
        public required string Cnpj { get; set; }
        public required string NomeFantasia { get; set; }
        public required int EnderecoId{ get; set; }
        [JsonIgnore]
        public virtual Endereco Endereco { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}

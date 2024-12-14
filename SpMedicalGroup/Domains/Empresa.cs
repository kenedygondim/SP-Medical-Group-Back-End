namespace SpMedicalGroup.Domains
{
    public class Empresa
    {
        public required string Cnpj { get; set; }
        public required string NomeFantasia { get; set; }
        public required int EnderecoId{ get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}

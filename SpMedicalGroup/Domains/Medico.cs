using Microsoft.AspNetCore.Http.HttpResults;

namespace SpMedicalGroup.Domains
{
    public class Medico
    {
        public required string Cpf { get; set; }
        public required string NomeCompleto { get; set; }
        public required string Rg { get; set; }
        public required string DataNascimento { get; set; }
        public string Crm { get; set; }
        public required int UsuarioId { get; set; }
        public required int EnderecoId { get; set; }
        public required int? FotoPerfilId { get; set; }
        public string? cnpj_empresa { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual FotoPerfil FotoPerfil { get; set; }
        public virtual ICollection<MedicoEspecialidade> MedicoEspecialidade { get; set; }
        public ICollection<Disponibilidade> Disponibilidades { get; set; }
    }
}

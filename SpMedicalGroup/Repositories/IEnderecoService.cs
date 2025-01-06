using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IEnderecoService
    {
        Task<Endereco> CadastrarEndereco(Endereco endereco);
    }
}

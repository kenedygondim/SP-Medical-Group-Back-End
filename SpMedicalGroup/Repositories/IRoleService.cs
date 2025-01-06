using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IRoleService
    {
        Task<Role> Cadastrar(Role novaRole);
        Task<List<Role>> ListarTodas();
    }
}
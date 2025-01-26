using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IRoleService
    {
        Task<string> AdicionarRole(string nomeRole);
        Task<List<Role>> GetAllRoles();
    }
}
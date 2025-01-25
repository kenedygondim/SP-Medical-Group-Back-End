using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IRoleService
    {
        Task<Role> AdicionarrRole(Role novaRole);
        Task<List<Role>> GetAllRoles();
    }
}
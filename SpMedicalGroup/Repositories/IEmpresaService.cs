using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IEmpresaService
    {
        Task<Empresa> CadastrarEmpresa(Empresa novaEmpresa);
        Task<List<Empresa>> GetAllEmpresas();
    }
}

using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IEmpresaService
    {
        Task<Empresa> Cadastrar(Empresa novaEmpresa);
        Task<List<Empresa>> ListarTodas();
    }
}

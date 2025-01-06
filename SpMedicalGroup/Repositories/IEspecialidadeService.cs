using SpMedicalGroup.Dto.Especialidade;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IEspecialidadeService
    {
        Task<List<IdNomeEspecialidadeDto>> ObterEspecialidadesMedico(string cpf);
        Task<List<PaginaEspecialidadesDto>> ListarInfoPaginaEspecialidades();
        Task<List<Especialidade>> ListarTodas();
    }
}

using SpMedicalGroup.Dto.Especialidade;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IEspecialidadeService
    {
        Task<List<IdNomeEspecialidadeDto>> GetAllEspecialidadesMedico(string cpf);
        Task<List<PaginaEspecialidadesDto>> GetDetalhesEspecialidades();
        Task<List<Especialidade>> GetAllEspecialidades();
    }
}

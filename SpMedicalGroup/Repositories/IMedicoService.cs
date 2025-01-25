using SpMedicalGroup.Dto.Medico;
using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IMedicoService
    {
        Task<InformacoesMedicoPopUp> GetDetalhesMedico(string cpfMedico);
        Task<List<MedicoInformacoesCardDto>> GetInfoBasicasMedico(string? especialidade, string? nomeMedico, string? numCrm);
        Task<string> BuscaCpfMedicoPorEmail(string email);
        Task<List<Medico>> GetAllMedicos();
        Task<InfoBasicasUsuario> GetInfoBasicasUsuarioMedico(string email);
        Task<PerfilCompletoMedicoDto> GetPerfilCompletoMedico(string email);
    }
}

using SpMedicalGroup.Dto.Medico;
using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IMedicoService
    {
        Task<InformacoesMedicoPopUp> InformacoesMedicoEspecifico(string cpfMedico);
        Task<List<MedicoInformacoesCardDto>> ListarInformacoesBasicasMedico(string? especialidade, string? nomeMedico, string? numCrm);
        Task<string> BuscaCpfMedicoPorEmail(string email);
        Task<List<Medico>> ListarTodos();
        Task<InfoBasicasUsuario> InfoBasicasUsuario(string email);
        Task<PerfilCompletoMedicoDto> PerfilCompletoMedico(string email);
    }
}

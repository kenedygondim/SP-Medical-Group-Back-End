using SpMedicalGroup.Dto.Consulta;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IConsultaService
    {
        Task<List<ConsultaDetalhadaDto>> GetAllConsultasMedico(string emailMedico);
        Task<List<ConsultaDetalhadaDto>> GetAllConsultasPaciente(string email);
        Task<ConfirmarConsultaDetalhesDto> GetDetalhesConsulta(string cpf, string nomeEspecialidade);
        Task<Consulta> AgendarConsulta(AgendarConsultaDto novaConsulta);
        Task<string> CancelarConsulta(int consultaId);
        Task<bool> VerificaDisponibilidadeJaPreenchida(int disponibilidadeId);
        Task<string> MarcarConsultaComoConcluida(int consultaId);
    }
}

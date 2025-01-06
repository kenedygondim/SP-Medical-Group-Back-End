using SpMedicalGroup.Dto.Consulta;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IConsultaService
    {
        Task<List<ConsultaDetalhadaDto>> ListarTodosConsultasMedico(string emailMedico);
        Task<ConfirmarConsultaDetalhesDto> ConfirmarConsultaDetalhes(string cpf, string nomeEspecialidade);
        Task<Consulta> Agendar(AgendarConsultaDto novaConsulta);
        Task<bool> VerificaDisponibilidadeJaPreenchida(int disponibilidadeId);
        Task<List<ConsultaDetalhadaDto>> ListarTodasConsultasPaciente(string email);
        Task<string> CancelarConsulta(int consultaId);
        Task<string> MarcarConsultaComoConcluida(int consultaId);
    }
}

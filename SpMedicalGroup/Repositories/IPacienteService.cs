using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IPacienteService
    {
        Task<Paciente> GetPacienteByEmail(string email);
        Task<string> BuscaCpfPacientePorEmail(string email);
        Task<Paciente> CadastrarPaciente(CadastroPacienteDto novoPaciente);
        Task<Paciente> AdicionarPaciente(Paciente paciente);
        Task<InfoBasicasUsuario> InfoBasicasUsuario(string email);
        Task<PerfilPacienteDto> PerfilCompletoPaciente(string email);
        Task<List<InfoBasicasUsuario>> ListarInformacoesBasicasPaciente(string emailMedico, string? especialidade, string? nomePaciente, string? dataAtendimento);
    }
}

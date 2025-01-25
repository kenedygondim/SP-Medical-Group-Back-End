using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IPacienteService
    {
        Task<InfoBasicasUsuario> GetInfoBasicasUsuarioPaciente(string email);
        Task<PerfilPacienteDto> GetPerfilCompletoPaciente(string email);
        Task<List<InfoBasicasUsuario>> GetInfoBasicasPaciente(string emailMedico, string? especialidade, string? nomePaciente, string? dataAtendimento);
        Task<Paciente> CadastrarPaciente(CadastroPacienteDto novoPaciente);
        Task<Paciente> AdicionarPaciente(Paciente paciente);
        Task<string> BuscaCpfPacientePorEmail(string email);
    }
}

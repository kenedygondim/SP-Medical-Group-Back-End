using SpMedicalGroup.Dto.Disponibilidade;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IDisponibilidadeService
    {
        Task<Disponibilidade> AdicionarDisponibilidade(CriarDisponibilidadeDto novaDisponibilidade);
        Task<Disponibilidade> ExcluirDisponibilidade(int disponibilidadeId);
        Task<List<Disponibilidade>> GetDisponibilidadesMedicoByData(string cpf, string data, bool situacaoNull);
        void VerificarValidadeData(CriarDisponibilidadeDto novaDisponibilidade);
        void VerificarDisponibilidadeJaPreenchida(List<Disponibilidade> disponibilidadesMedico, CriarDisponibilidadeDto novaDisponibilidade);
    }
}

using SpMedicalGroup.Dto.Disponibilidade;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IDisponibilidadeService
    {
        Task<Disponibilidade> AdicionarDisponibilidade(CriarDisponibilidadeDto novaDisponibilidade);
        Task<List<Disponibilidade>> ListarDisponibilidadesMedicoPorData(string cpf, string data, bool situacaoNull);
        void VerificarValidadeData(CriarDisponibilidadeDto novaDisponibilidade);
        void VerificarDisponibilidadeJaPreenchida(List<Disponibilidade> disponibilidadesMedico, CriarDisponibilidadeDto novaDisponibilidade);
    }
}

using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Dto;

namespace SpMedicalGroup.Models
{
    public class DisponibilidadeModel
    {
        private readonly SpMedicalGroupContext ctx = new();

        public async Task adicionarDisponibilidade(CriarDisponibilidadeDto novaDisponibilidade)
        {
            Disponibilidade disponibilidade = new()
            {
                CpfMedico = novaDisponibilidade.CpfMedico,
                DataDisp = novaDisponibilidade.DataDisp,
                HoraInicio = novaDisponibilidade.HoraInicio,
                HoraFim = novaDisponibilidade.HoraFim
            };

            await ctx.Disponibilidades.AddAsync(disponibilidade);
            await ctx.SaveChangesAsync();
        }


    }
}

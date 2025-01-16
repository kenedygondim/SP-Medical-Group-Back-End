using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Consulta;
using SpMedicalGroup.Repositories;

namespace SpMedicalGroup.Services
{
    public class ConsultaService : IConsultaService
    {

        private readonly SpMedicalGroupContext ctx;
        private readonly IPacienteService pacienteService;

        public ConsultaService(SpMedicalGroupContext ctx, IPacienteService pacienteService)
        {
            this.ctx = ctx;
            this.pacienteService = pacienteService;
        }

        public async Task<List<ConsultaDetalhadaDto>> ListarTodosConsultasMedico(string emailMedico)
        {

            List<ConsultaDetalhadaDto> consultasMedico =
                await ctx.Set<ConsultaDetalhadaDto>()
                .FromSql($"EXEC Consultas_Medico_By_Email {emailMedico}")
                .ToListAsync();

            return consultasMedico;
        }

        // TO-DO: fix provisório, alterar depois
        public async Task<ConfirmarConsultaDetalhesDto> ConfirmarConsultaDetalhes(string cpf, string nomeEspecialidade)
        {
            var confirmarConsultaDetalhes =
             await ctx.Set<ConfirmarConsultaDetalhesDto>()
             .FromSqlRaw("EXEC Preview_Detalhes_Consulta @p0, @p1", cpf, nomeEspecialidade)
             .IgnoreQueryFilters()
             .ToListAsync();

            return confirmarConsultaDetalhes[0];
        }

        public async Task<Consulta> Agendar(AgendarConsultaDto novaConsulta)
        {
            bool disponibilidadeUtilizavel = await VerificaDisponibilidadeJaPreenchida(novaConsulta.DisponibilidadeId);

            if (!disponibilidadeUtilizavel)
                throw new Exception("Disponibilidade já preenchida");

            string cpfPaciente = await pacienteService.BuscaCpfPacientePorEmail(novaConsulta.EmailPaciente) ?? throw new Exception();

            Consulta consultaCriada = new()
            {
                EspecialidadeId = novaConsulta.EspecialidadeId,
                DisponibilidadeId = novaConsulta.DisponibilidadeId,
                CpfPaciente = cpfPaciente,
                Descricao = novaConsulta.Descricao,
                Situacao = "Agendada",
                IsTelemedicina = novaConsulta.IsTelemedicina
            };

            await ctx.Consulta.AddAsync(consultaCriada);
            await ctx.SaveChangesAsync();

            return consultaCriada;
        }

        public async Task<bool> VerificaDisponibilidadeJaPreenchida(int disponibilidadeId)
        {
            return await ctx.Consulta
                .Where(c => c.DisponibilidadeId == disponibilidadeId)
                .FirstOrDefaultAsync() == null;
        }

        public async Task<List<ConsultaDetalhadaDto>> ListarTodasConsultasPaciente(string emailPaciente)
        {
            List<ConsultaDetalhadaDto> consultasPaciente =
                await ctx.Set<ConsultaDetalhadaDto>()
                .FromSql($"EXEC Consultas_Paciente_By_Email {emailPaciente}")
                .ToListAsync();

            return consultasPaciente;
        }

        public async Task<string> CancelarConsulta(int consultaId)
        {
            var consulta = await ctx.Consulta
                .Where(c => c.ConsultaId == consultaId)
                .FirstOrDefaultAsync() ?? throw new Exception("Consulta não encontrada");
            ctx.Consulta.Remove(consulta);
            await ctx.SaveChangesAsync();
            return "Cancelada com sucesso!";
        }

        public async Task<string> MarcarConsultaComoConcluida(int consultaId)
        {
            var consulta = await ctx.Consulta
                .Where(c => c.ConsultaId == consultaId)
                .FirstOrDefaultAsync() ?? throw new Exception("Consulta não encontrada");
            consulta.Situacao = "Concluída";
            ctx.Consulta.Update(consulta);
            await ctx.SaveChangesAsync();
            return "Consulta marcada como concluída!";
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Disponibilidade;
using SpMedicalGroup.Repositories;

namespace SpMedicalGroup.Services
{
    public class DisponibilidadeService : IDisponibilidadeService
    {
        private readonly SpMedicalGroupContext ctx;

        public DisponibilidadeService(SpMedicalGroupContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Disponibilidade> AdicionarDisponibilidade(CriarDisponibilidadeDto novaDisponibilidade)
        {

            VerificarValidadeData(novaDisponibilidade);

            var cpfMedico =
                (from tb_usuarios in ctx.Usuarios
                 join tb_medicos in ctx.Medicos
                 on tb_usuarios.UsuarioId equals tb_medicos.UsuarioId
                 where tb_usuarios.Email == novaDisponibilidade.EmailMedico
                 select tb_medicos.Cpf).FirstOrDefault() ?? throw new Exception("Médico não encontrado");

            List<Disponibilidade> disponibilidadesMedico = await ListarDisponibilidadesMedicoPorData(cpfMedico, novaDisponibilidade.DataDisp, false);

            VerificarDisponibilidadeJaPreenchida(disponibilidadesMedico, novaDisponibilidade);

            Disponibilidade disponibilidade = new()
            {
                CpfMedico = cpfMedico,
                DataDisp = novaDisponibilidade.DataDisp,
                HoraInicio = novaDisponibilidade.HoraInicio,
                HoraFim = novaDisponibilidade.HoraFim
            };

            await ctx.Disponibilidades.AddAsync(disponibilidade);
            await ctx.SaveChangesAsync();

            return disponibilidade;
        }


        public async Task<List<Disponibilidade>> ListarDisponibilidadesMedicoPorData(string cpf, string data, bool situacaoNull) // situacaoNull = true: retorna apenas as disponibilidades que ainda não foram agendadas (Verificação importante para reutilização de código)
        {   
            var disponibilidadePorData = await
                (from dis in ctx.Disponibilidades
                 join con in ctx.Consulta on dis.DisponibilidadeId equals con.DisponibilidadeId into consultas
                 from con in consultas.DefaultIfEmpty() // Left join
                 join med in ctx.Medicos on dis.CpfMedico equals med.Cpf
                 where med.Cpf == cpf && dis.DataDisp == data && (!situacaoNull || con.Situacao == null)
                 select dis).ToListAsync();

            return disponibilidadePorData;
        }


        public void VerificarValidadeData(CriarDisponibilidadeDto novaDisponibilidade)
        {
            DateTime data = DateTime.Parse(novaDisponibilidade.DataDisp);
            TimeSpan horaInicio = TimeSpan.Parse(novaDisponibilidade.HoraInicio);
            TimeSpan horaFim = TimeSpan.Parse(novaDisponibilidade.HoraFim);

            DateTime dataAtual = DateTime.Today;
            TimeSpan horaAtual = DateTime.Now.TimeOfDay;

            if (dataAtual > data)
                throw new Exception("A data da disponibilidade não pode ser anterior à data atual");
            else if (dataAtual == data && horaAtual > horaInicio)
                throw new Exception("A hora de início não pode ser anterior à hora atual");
            else if (horaInicio >= horaFim)
                throw new Exception("A hora de início deve ser anterior à hora de término");
            else if (data > dataAtual.AddDays(365))
                throw new Exception("A data da disponibilidade não pode ser superior a 365 dias da data atual");
        }

        public void VerificarDisponibilidadeJaPreenchida(List<Disponibilidade> disponibilidadesMedico, CriarDisponibilidadeDto novaDisponibilidade)
        {
            TimeSpan horaInicio = TimeSpan.Parse(novaDisponibilidade.HoraInicio);
            TimeSpan horaFim = TimeSpan.Parse(novaDisponibilidade.HoraFim);

            foreach (Disponibilidade disp in disponibilidadesMedico)
            {
                TimeSpan horaInicioDispExistente = TimeSpan.Parse(disp.HoraInicio);
                TimeSpan horaFimDispExistente = TimeSpan.Parse(disp.HoraFim);

                if ((horaInicio >= horaInicioDispExistente && horaInicio < horaFimDispExistente) ||  // Começa dentro de um intervalo existente
                    (horaFim > horaInicioDispExistente && horaFim <= horaFimDispExistente) ||        // Termina dentro de um intervalo existente
                    (horaInicio <= horaInicioDispExistente && horaFim >= horaFimDispExistente))     // Engloba um intervalo existente
                {
                    throw new Exception("Já existe uma disponibilidade cadastrada para o médico nesse horário");
                }
            }
        }

        public async Task<Disponibilidade> ExcluirDisponibilidade(int disponibilidadeId)
        {
            Disponibilidade disponibilidade = await ctx.Disponibilidades.FindAsync(disponibilidadeId) ?? throw new Exception("Disponibilidade não encontrada");

            ctx.Disponibilidades.Remove(disponibilidade);
            await ctx.SaveChangesAsync();

            return disponibilidade;
        }
    }
}

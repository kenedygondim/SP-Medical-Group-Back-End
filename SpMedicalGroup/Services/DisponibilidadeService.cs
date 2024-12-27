using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Disponibilidade;

namespace SpMedicalGroup.Services
{
    public class DisponibilidadeService
    {
        private readonly SpMedicalGroupContext ctx = new();

        public async Task<Disponibilidade> AdicionarDisponibilidade(CriarDisponibilidadeDto novaDisponibilidade)
        {
            var cpfMedico =
                (from tb_usuarios in ctx.Usuarios
                 join tb_medicos in ctx.Medicos
                 on tb_usuarios.UsuarioId equals tb_medicos.UsuarioId
                 where tb_usuarios.Email == novaDisponibilidade.EmailMedico
                 select tb_medicos.Cpf).FirstOrDefault() ?? throw new Exception("Médico não encontrado");

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

        public async Task<List<Disponibilidade>> ListarDisponibilidadesMedicoPorData(string cpf, string data)
        {
            var disponibilidadePorData = await
                (from dis in ctx.Disponibilidades
                 join con in ctx.Consulta on dis.DisponibilidadeId equals con.DisponibilidadeId into consultas
                 from con in consultas.DefaultIfEmpty() // Left join
                 join med in ctx.Medicos on dis.CpfMedico equals med.Cpf
                 where med.Cpf == cpf && dis.DataDisp == data && con.Situacao == null
                 select dis).ToListAsync();

            return disponibilidadePorData;
        }

    }
}

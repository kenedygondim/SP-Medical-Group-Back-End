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
            var cpfMedico =
                (from tb_usuarios in ctx.Usuarios
                 join tb_medicos in ctx.Medicos
                 on tb_usuarios.UsuarioId equals tb_medicos.UsuarioId
                 where tb_usuarios.Email == novaDisponibilidade.emailMedico
                 select tb_medicos.Cpf).FirstOrDefault();


            if (cpfMedico == null)
            {
                throw new Exception("Médico não encontrado");
            }

            Disponibilidade disponibilidade = new()
            {
                CpfMedico = cpfMedico,
                DataDisp = novaDisponibilidade.DataDisp,
                HoraInicio = novaDisponibilidade.HoraInicio,
                HoraFim = novaDisponibilidade.HoraFim
            };

            await ctx.Disponibilidades.AddAsync(disponibilidade);
            await ctx.SaveChangesAsync();
        }


    }
}

using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Medico;
using SpMedicalGroup.Dto.Paciente;


namespace SpMedicalGroup.Services
{
    public class MedicoService
    {
        private static readonly SpMedicalGroupContext ctx = new();

        public async Task<InformacoesMedicoPopUp> InformacoesMedicoEspecifico(string cpfMedico)
        {
            var informacoesMedico = await 
                (from med in ctx.Medicos
                join fot in ctx.FotosPerfil on med.FotoPerfilId equals fot.FotoPerfilId
                join emp in ctx.Empresas on med.cnpj_empresa equals emp.Cnpj
                join usu in ctx.Usuarios on med.UsuarioId equals usu.UsuarioId
                join dis in ctx.Disponibilidades on med.Cpf equals dis.CpfMedico into disponibilidades
                from disLeft in disponibilidades.DefaultIfEmpty()
                join con in ctx.Consulta on disLeft.DisponibilidadeId equals con.DisponibilidadeId into consultas
                from conLeft in consultas.DefaultIfEmpty()
                where med.Cpf == cpfMedico
                group new { med, fot, emp, usu, conLeft } by new
                {
                    med.Cpf,
                    med.NomeCompleto,
                    med.Crm,
                    med.DataNascimento,
                    fot.FotoPerfilUrl,
                    emp.NomeFantasia,
                    usu.Email
                } into grouped
                select new InformacoesMedicoPopUp
                {
                    Cpf = grouped.Key.Cpf,
                    NomeCompleto = grouped.Key.NomeCompleto,
                    Crm = grouped.Key.Crm,
                    DataNascimento = grouped.Key.DataNascimento,
                    Email = grouped.Key.Email,
                    FotoPerfilUrl = grouped.Key.FotoPerfilUrl,
                    NomeFantasia = grouped.Key.NomeFantasia,
                    NumeroConsultas = grouped.Count(g => g.conLeft != null)
                }).FirstOrDefaultAsync() ?? throw new Exception("Médico não encontrado.");

            return informacoesMedico;
        }

        public async Task<List<MedicoInformacoesCardDto>> ListarInformacoesBasicasMedico(string? especialidade, string? nomeMedico, string? numCrm)
        {
            var query = from med in ctx.Medicos
                        join fot in ctx.FotosPerfil on med.FotoPerfilId equals fot.FotoPerfilId
                        join emp in ctx.Empresas on med.cnpj_empresa equals emp.Cnpj
                        select new MedicoInformacoesCardDto
                        {
                            Cpf = med.Cpf,
                            NomeCompleto = med.NomeCompleto,
                            Crm = med.Crm,
                            FotoPerfilUrl = fot.FotoPerfilUrl ?? "",
                            NomeFantasia = emp.NomeFantasia
                        };

            if (!string.IsNullOrEmpty(especialidade))
            {
                query = from med in query
                        join medEsp in ctx.MedicosEspecialidades on med.Cpf equals medEsp.CpfMedico
                        join esp in ctx.Especialidades on medEsp.EspecialidadeId equals esp.EspecialidadeId
                        where esp.Nome.Contains(especialidade)
                        select med;
            }
            if (!string.IsNullOrEmpty(nomeMedico))
                query = query.Where(med => med.NomeCompleto.Contains(nomeMedico));

            if (!string.IsNullOrEmpty(numCrm))
                query = query.Where(med => med.Crm.Contains(numCrm));

            return await query.ToListAsync();
        }

        public static async Task<string> BuscaCpfMedicoPorEmail(string email)
        {
            var cpfMedico = await
            (from tb_usuarios in ctx.Usuarios
                join tb_medicos in ctx.Medicos
                on tb_usuarios.UsuarioId equals tb_medicos.UsuarioId
                where tb_usuarios.Email == email
                select tb_medicos.Cpf).FirstOrDefaultAsync() ?? throw new Exception("Médico não encontrado.");

            return cpfMedico;
        }

        public async Task<List<Medico>> ListarTodos()
        {
             return await ctx.Medicos.ToListAsync();
        }

        public async Task<InfoBasicasUsuario> InfoBasicasUsuario(string email)
        {
            var medico = await
                (from usu in ctx.Usuarios
                 join med in ctx.Medicos on usu.UsuarioId equals med.UsuarioId
                 join foto in ctx.FotosPerfil on med.FotoPerfilId equals foto.FotoPerfilId into fotos
                 from fotoLeft in fotos.DefaultIfEmpty()
                 where usu.Email == email
                 select new InfoBasicasUsuario
                 {
                     Cpf = med.Cpf,
                     NomeCompleto = med.NomeCompleto,
                     FotoPerfilUrl = fotoLeft.FotoPerfilUrl ?? ""
                 }).FirstOrDefaultAsync() ?? throw new Exception("Paciente não encontrado");
            return medico;
        }
    }
}

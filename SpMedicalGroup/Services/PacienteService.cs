using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Paciente;

namespace SpMedicalGroup.Services
{
    public class PacienteService
    {
        private static readonly SpMedicalGroupContext ctx = new();
        private readonly EnderecoService enderecoService = new();
        private readonly UsuarioService usuarioService = new();

        //public async Task<List<InfoBasicasPaciente>> ListarPacientesMedico(string emailUsuario)
        //{
        //    string cpfMedico = await MedicoService.BuscaCpfMedicoPorEmail(emailUsuario);

        //    var pacientesMedico = await
        //        (from pac in ctx.Pacientes
        //         join con in ctx.Consulta on pac.Cpf equals con.CpfPaciente
        //         join dis in ctx.Disponibilidades on con.DisponibilidadeId equals dis.DisponibilidadeId
        //         join med in ctx.Medicos on dis.CpfMedico equals med.Cpf
        //         join esp in ctx.Especialidades on con.EspecialidadeId equals esp.EspecialidadeId
        //         join medEsp in ctx.MedicosEspecialidades
        //             on new { CpfMedico = med.Cpf, EspecialidadeId = esp.EspecialidadeId }
        //             equals new { medEsp.CpfMedico, medEsp.EspecialidadeId }
        //         where med.Cpf == cpfMedico
        //         select new InfoBasicasPaciente
        //         {
        //             Cpf = pac.Cpf,
        //             NomeCompleto = pac.NomeCompleto,

        //         }).ToListAsync();

        //    return pacientesMedico;
        //}

        public static async Task<string> BuscaCpfPacientePorEmail(string email)
        {
            var cpfPaciente = await
            (from tb_usuarios in ctx.Usuarios
             join tb_pacientes in ctx.Pacientes
             on tb_usuarios.UsuarioId equals tb_pacientes.UsuarioId
             where tb_usuarios.Email == email
             select tb_pacientes.Cpf).FirstOrDefaultAsync() ?? throw new Exception("Médico não encontrado");

            return cpfPaciente;
        }

        public async Task<Paciente> CadastrarPaciente(PerfilCompletoPacienteDto novoPaciente)
        {

            using var transaction = ctx.Database.BeginTransaction();
            try
            {
                Usuario usuario = new()
                {
                    Email = novoPaciente.Email,
                    Senha = novoPaciente.Senha,
                    RoleId = 1
                };

                Usuario usuarioCriado = await usuarioService.CadastrarUsuario(usuario);

                Endereco endereco = new()
                {
                    Cep = novoPaciente.Cep,
                    Logradouro = novoPaciente.Logradouro,
                    Numero = novoPaciente.Numero,
                    Bairro = novoPaciente.Bairro,
                    Municipio = novoPaciente.Municipio,
                    Uf = novoPaciente.Uf,
                    Complemento = novoPaciente.Complemento
                };

                Endereco enderecoCriado = await enderecoService.CadastrarEndereco(endereco);

                Paciente paciente = new()
                {
                    NomeCompleto = novoPaciente.NomeCompleto,
                    DataNascimento = novoPaciente.DataNascimento,
                    Rg = novoPaciente.Rg,
                    Cpf = novoPaciente.Cpf,
                    EnderecoId = enderecoCriado.EnderecoId,
                    UsuarioId = usuarioCriado.UsuarioId

                };

                Paciente pacienteCriado = await AdicionarPaciente(paciente);

                await transaction.CommitAsync();

                return pacienteCriado;

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }


        public async Task<Paciente> AdicionarPaciente(Paciente paciente)
        {
            try
            {
                await ctx.Pacientes.AddAsync(paciente);
                await ctx.SaveChangesAsync();
                return paciente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InfoBasicasUsuario> InfoBasicasUsuario(string email)
        {
            var paciente = await
                (from usu in ctx.Usuarios
                 join pac in ctx.Pacientes on usu.UsuarioId equals pac.UsuarioId
                 join foto in ctx.FotosPerfil on pac.FotoPerfilId equals foto.FotoPerfilId into fotos
                 from fotoLeft in fotos.DefaultIfEmpty()
                 where usu.Email == email
                 select new InfoBasicasUsuario
                 {
                     Cpf = pac.Cpf,
                     NomeCompleto = pac.NomeCompleto,
                     FotoPerfilUrl = fotoLeft.FotoPerfilUrl ?? ""
                 }).FirstOrDefaultAsync() ?? throw new Exception("Paciente não encontrado");
            return paciente;
        }

        public async Task<PerfilCompletoPacienteDto> PerfilCompletoPaciente(string email)
        {
            return await
                (from usu in ctx.Usuarios
                 join pac in ctx.Pacientes on usu.UsuarioId equals pac.UsuarioId
                 join end in ctx.Enderecos on pac.EnderecoId equals end.EnderecoId
                 join foto in ctx.FotosPerfil on pac.FotoPerfilId equals foto.FotoPerfilId into fotos
                 from fotoLeft in fotos.DefaultIfEmpty()
                 where usu.Email == email
                 select new PerfilCompletoPacienteDto
                 {
                     NomeCompleto = pac.NomeCompleto,
                     DataNascimento = pac.DataNascimento,
                     Rg = pac.Rg,
                     Cpf = pac.Cpf,
                     Email = usu.Email,
                     Cep = end.Cep,
                     Logradouro = end.Logradouro,
                     Numero = end.Numero,
                     Bairro = end.Bairro,
                     Municipio = end.Municipio,
                     Uf = end.Uf,
                     Complemento = end.Complemento ?? "",
                     FotoPerfilUrl = fotoLeft.FotoPerfilUrl ?? ""
                 }).FirstOrDefaultAsync() ?? throw new Exception("Paciente não encontrado");         
        }

        public async Task<List<InfoBasicasUsuario>> ListarInformacoesBasicasPaciente(string emailMedico, string? especialidade, string? nomePaciente, string? dataAtendimento)
        {
            var query = from pac in ctx.Pacientes
                        join fot in ctx.FotosPerfil on pac.FotoPerfilId equals fot.FotoPerfilId into fotos
                        from fot in fotos.DefaultIfEmpty()
                        select new InfoBasicasUsuario
                        {
                            Cpf = pac.Cpf,
                            NomeCompleto = pac.NomeCompleto,
                            FotoPerfilUrl = fot.FotoPerfilUrl ?? ""
                        };

            if (!string.IsNullOrEmpty(especialidade))
            {
                //query = from med in query
                //        join medEsp in ctx.MedicosEspecialidades on med.Cpf equals medEsp.CpfMedico
                //        join esp in ctx.Especialidades on medEsp.EspecialidadeId equals esp.EspecialidadeId
                //        where esp.Nome.Contains(especialidade)
                //        select med;
            }
            if (!string.IsNullOrEmpty(nomePaciente))
                query = query.Where(pac => pac.NomeCompleto.Contains(nomePaciente));

            if (!string.IsNullOrEmpty(dataAtendimento)) { }
            //query = query.Where(med => med.Crm.Contains(numCrm));


            return await query.ToListAsync();
        }
    }
}
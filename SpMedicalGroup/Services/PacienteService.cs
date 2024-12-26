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

        public async Task<List<NomeECpfDoPacienteDto>> ListarPacientesMedico(string emailUsuario)
        {
            string cpfMedico = await MedicoService.BuscaCpfMedicoPorEmail(emailUsuario);

            var pacientesMedico = await 
                (from pac in ctx.Pacientes
                join con in ctx.Consulta on pac.Cpf equals con.CpfPaciente
                join dis in ctx.Disponibilidades on con.DisponibilidadeId equals dis.DisponibilidadeId
                join med in ctx.Medicos on dis.CpfMedico equals med.Cpf
                join esp in ctx.Especialidades on con.EspecialidadeId equals esp.EspecialidadeId
                join medEsp in ctx.MedicosEspecialidades
                    on new { CpfMedico = med.Cpf, EspecialidadeId = esp.EspecialidadeId } 
                    equals new { medEsp.CpfMedico, medEsp.EspecialidadeId }
                where med.Cpf == cpfMedico
                select new NomeECpfDoPacienteDto
                {
                    Cpf = pac.Cpf,
                    NomeCompleto = pac.NomeCompleto
                }).ToListAsync();

            return pacientesMedico;
        }

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

        public async Task<Paciente> CadastrarPaciente (CadastroPacienteDto novoPaciente)
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

    }
}

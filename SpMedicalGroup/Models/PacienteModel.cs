using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalGroup.Models
{
    public class PacienteModel
    {
        private static readonly SpMedicalGroupContext ctx = new SpMedicalGroupContext();
        private static readonly EnderecoModel enderecoModel = new();
        private static readonly UsuarioModel usuarioModel = new();

        public List<Paciente> ListarTodos()
        {
            return ctx.Pacientes.ToList();
        }

        public async Task<List<NomeECpfDoPacienteDto>> ListarPacientesMedico(string emailUsuario)
        {
            try
            {
                string cpfMedico = await MedicoModel.buscaCpfMedicoPorEmail(emailUsuario);

                var sql = @"
                    SELECT 
                        PAC.cpf AS Cpf,
                        PAC.nome_completo AS NomeCompleto
                    FROM tb_pacientes PAC
                    JOIN tb_consultas CON ON CON.cpf_paciente = PAC.cpf
                    JOIN tb_disponibilidades DIS ON DIS.disponibilidade_id = CON.disponibilidade_id
                    JOIN tb_medicos MED ON MED.cpf = DIS.cpf_medico
                    JOIN tb_especialidades ESP ON ESP.especialidade_id = CON.especialidade_id
                    JOIN tb_medico_especialidades MEDESP ON MEDESP.cpf_medico = MED.cpf AND MEDESP.especialidade_id = ESP.especialidade_id
                    WHERE MED.cpf = {0}
                ";

                return ctx.Set<NomeECpfDoPacienteDto>().FromSqlRaw(sql, cpfMedico).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<string> buscaCpfPacientePorEmail(string email)
        {
            try
            {
                var cpfPaciente =
                (from tb_usuarios in ctx.Usuarios
                 join tb_pacientes in ctx.Pacientes
                 on tb_usuarios.UsuarioId equals tb_pacientes.UsuarioId
                 where tb_usuarios.Email == email
                 select tb_pacientes.Cpf).FirstOrDefault();

                if (cpfPaciente == null)
                    throw new Exception("Médico não encontrado");

                return cpfPaciente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
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

                Usuario usuarioCriado = await usuarioModel.CadastrarUsuario(usuario);

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

                Endereco enderecoCriado = await enderecoModel.CadastrarEndereco(endereco);

                Paciente paciente = new Paciente
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

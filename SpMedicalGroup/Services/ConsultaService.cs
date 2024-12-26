using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Consulta;

namespace SpMedicalGroup.Services
{
    public class ConsultaService
    {

        private readonly SpMedicalGroupContext ctx = new();

        public async Task<List<ConsultaDetalhadaDto>> ListarTodosConsultasMedico(string emailMedico)
        {
            var cpfMedico = await MedicoService.BuscaCpfMedicoPorEmail(emailMedico) ?? throw new Exception();

            var consultasMedico = await 
                (from pac in ctx.Pacientes
                join con in ctx.Consulta on pac.Cpf equals con.CpfPaciente
                join dis in ctx.Disponibilidades on con.DisponibilidadeId equals dis.DisponibilidadeId
                join med in ctx.Medicos on dis.CpfMedico equals med.Cpf
                join esp in ctx.Especialidades on con.EspecialidadeId equals esp.EspecialidadeId
                join medEsp in ctx.MedicosEspecialidades
                    on new { CpfMedico = med.Cpf, esp.EspecialidadeId }
                    equals new { medEsp.CpfMedico, medEsp.EspecialidadeId }
                where med.Cpf == cpfMedico
                select new ConsultaDetalhadaDto
                {
                    NomePaciente = pac.NomeCompleto,
                    CpfPaciente = pac.Cpf,
                    NomeMedico = med.NomeCompleto,
                    Especialidade = esp.Nome,
                    DataConsulta = dis.DataDisp,
                    HoraInicio = dis.HoraInicio,
                    HoraFim = dis.HoraFim,
                    Preco = medEsp.ValorProcedimento,
                    Situacao = con.Situacao
                }).ToListAsync();

            return consultasMedico;
        }

        public async Task<ConfirmarConsultaDetalhesDto> ConfirmarConsultaDetalhes(string cpf, string nomeEspecialidade) 
        {
            var consultaDetalhes = await 
                (from medEsp in ctx.MedicosEspecialidades
                join esp in ctx.Especialidades on medEsp.EspecialidadeId equals esp.EspecialidadeId
                join med in ctx.Medicos on medEsp.CpfMedico equals med.Cpf
                join emp in ctx.Empresas on med.cnpj_empresa equals emp.Cnpj
                join ende in ctx.Enderecos on emp.EnderecoId equals ende.EnderecoId
                where med.Cpf == cpf && esp.Nome == nomeEspecialidade
                select new ConfirmarConsultaDetalhesDto
                {
                    valorConsulta = medEsp.ValorProcedimento,
                    nomeEmpresa = emp.NomeFantasia,
                    municipio = ende.Municipio,
                    bairro = ende.Bairro,
                    logradouro = ende.Logradouro,
                    numero = ende.Numero,
                    complemento = ende.Complemento ?? ""
                }).FirstOrDefaultAsync() ?? throw new Exception();

            return consultaDetalhes;
        }

        public async Task<Consulta> Agendar(Consulta novaConsulta)
        {
            await ctx.Consulta.AddAsync(novaConsulta);
            await ctx.SaveChangesAsync();
            return novaConsulta;
        }

        /* 
         public void Atualizar(byte id, Consulta consultaAtt)
         {
             Consulta consultaBuscada = BuscarPorId(id);

                 if (consultaAtt.IdMedico != null || consultaAtt.IdPaciente != null || consultaAtt.Descricao != null || consultaAtt.Situacao != null)
                 {
                     consultaBuscada.IdMedico = consultaAtt.IdMedico;
                     consultaBuscada.IdPaciente = consultaAtt.IdPaciente;
                     consultaBuscada.DataConsulta = consultaAtt.DataConsulta;
                     consultaBuscada.Descricao = consultaAtt.Descricao;
                     consultaBuscada.Situacao = consultaAtt.Situacao;

                     ctx.Consulta.Update(consultaBuscada);
                     ctx.SaveChanges();
                 }
         }



         public void CancelarConsulta(byte id)
         {
             Consulta consultaBuscada = BuscarPorId(id);

             consultaBuscada.Descricao = "Consulta cancelada";
             consultaBuscada.Situacao = "Cancelada";

             ctx.Consulta.Update(consultaBuscada);
             ctx.SaveChanges();

         }

         public Consulta BuscarPorId(byte id)
         {
             return ctx.Consulta.FirstOrDefault(e => e.IdConsulta == id);
         }

         public void Deletar(byte id)
         {
             ctx.Consulta.Remove(BuscarPorId(id));

             ctx.SaveChanges();
         }

         public void IncluirDescricao(byte id, string descricao)
         {
             Consulta consultaBuscada = BuscarPorId(id);

             if (descricao != null)
                 {
                     consultaBuscada.Descricao = descricao;

                     ctx.Consulta.Update(consultaBuscada);
                     ctx.SaveChanges();
                 }

         }

         public List<Consulta> LerTodasDoMedico(int idUsuario)
         {
             Medico medico = ctx.Medicos.FirstOrDefault(p => p.IdUsuario == idUsuario);
             short idMedico = medico.IdMedico;

             return ctx.Consulta
                 .Where(m => m.IdMedico == idMedico)
                 .Select(m => new Consulta()
                 {
                     DataConsulta = m.DataConsulta,
                     IdConsulta = m.IdConsulta,
                     IdMedicoNavigation = new Medico()
                     {
                         IdUsuarioNavigation = new Usuario()
                         {
                             NomeUsuario = m.IdMedicoNavigation.IdUsuarioNavigation.NomeUsuario
                         },

                         IdEmpresaNavigation = new Empresa()
                         {
                             Endereco = m.IdMedicoNavigation.IdEmpresaNavigation.Endereco
                         }


                     },
                     IdPacienteNavigation = new Paciente()
                     {
                         IdUsuarioNavigation = new Usuario()
                         {
                             NomeUsuario = m.IdPacienteNavigation.IdUsuarioNavigation.NomeUsuario
                         }
                     },
                     Descricao = m.Descricao,
                     Situacao = m.Situacao

                 })
                 .ToList();
         }

         public List<Consulta> LerTodasDoPaciente(int idUsuario)
         {
             Paciente paciente = ctx.Pacientes.FirstOrDefault(p => p.IdUsuario == idUsuario);
             short idPaciente = paciente.IdPaciente;

             return ctx.Consulta
                 .Where(p => p.IdPaciente == idPaciente)
                 .Select(p => new Consulta()
                 {
                     DataConsulta = p.DataConsulta,
                     IdConsulta = p.IdConsulta,
                     IdMedicoNavigation = new Medico()
                     {
                         IdUsuarioNavigation = new Usuario()
                         {
                             NomeUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.NomeUsuario
                         },

                         IdEmpresaNavigation = new Empresa()
                         {
                             Endereco = p.IdMedicoNavigation.IdEmpresaNavigation.Endereco
                         }


                     },
                     IdPacienteNavigation = new Paciente()
                     {
                         IdUsuarioNavigation = new Usuario()
                         {
                             NomeUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.NomeUsuario
                         }
                     },
                     Descricao = p.Descricao,
                     Situacao = p.Situacao

                 })
                 .ToList();
         }

         public List<Consulta> ListarTodos()
         {
             return ctx.Consulta
                 .Select(p => new Consulta()
                 {
                     DataConsulta = p.DataConsulta,
                     IdConsulta = p.IdConsulta,
                     IdMedicoNavigation = new Medico()
                     {
                         IdUsuarioNavigation= new Usuario()
                         {
                             NomeUsuario = p.IdMedicoNavigation.IdUsuarioNavigation.NomeUsuario
                         },

                         IdEmpresaNavigation = new Empresa()
                         {
                             Endereco = p.IdMedicoNavigation.IdEmpresaNavigation.Endereco
                         }
                     },
                     IdPacienteNavigation = new Paciente()
                     {
                         IdUsuarioNavigation = new Usuario()
                         {
                             NomeUsuario = p.IdPacienteNavigation.IdUsuarioNavigation.NomeUsuario
                         }
                     },
                     Descricao = p.Descricao,
                     Situacao = p.Situacao

                 })
                 .ToList();*/
    }
    }

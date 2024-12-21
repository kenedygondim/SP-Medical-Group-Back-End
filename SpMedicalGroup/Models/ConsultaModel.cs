using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpMedicalGroup.Models
{   
    public class ConsultaModel
    {

        private static readonly SpMedicalGroupContext ctx = new SpMedicalGroupContext();


        public async Task<List<ConsultaDetalhadaDto>> ListarTodosConsultasMedico(string emailMedico)
        {
            var cpfMedico = await MedicoModel.buscaCpfMedicoPorEmail(emailMedico);

            var sql = @"
                SELECT 
                    PAC.nome_completo AS NomePaciente,
                    PAC.cpf AS CpfPaciente,
                    MED.nome_completo AS NomeMedico,
                    ESP.nome AS Especialidade,
                    DIS.data_disp AS DataConsulta,
                    DIS.hora_inicio AS HoraInicio,
                    DIS.hora_fim AS HoraFim,
                    MEDESP.valor_procedimento AS Preco,
                    CON.situacao AS Situacao
                FROM tb_pacientes PAC
                JOIN tb_consultas CON ON CON.cpf_paciente = PAC.cpf
                JOIN tb_disponibilidades DIS ON DIS.disponibilidade_id = CON.disponibilidade_id
                JOIN tb_medicos MED ON MED.cpf = DIS.cpf_medico
                JOIN tb_especialidades ESP ON ESP.especialidade_id = CON.especialidade_id
                JOIN tb_medico_especialidades MEDESP ON MEDESP.cpf_medico = MED.cpf AND MEDESP.especialidade_id = ESP.especialidade_id
                WHERE MED.cpf = {0}
            ";

            return ctx.Set<ConsultaDetalhadaDto>()
            .FromSqlRaw(sql, cpfMedico)
            .ToList();
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

         public void Cadastrar(Consulta novaConsulta)
         {
             ctx.Consulta.Add(novaConsulta);          
             ctx.SaveChanges();
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

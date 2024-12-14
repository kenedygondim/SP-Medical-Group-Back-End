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

        SpMedicalGroupContext ctx = new SpMedicalGroupContext();
        



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

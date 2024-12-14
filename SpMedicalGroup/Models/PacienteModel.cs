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
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();

        public List<ConsultaDetalhadaDto> ListarTodosC()
        {
            var medicoNome = "Eduardo%"; // Filtro para o nome do médico
            var sql = @"
                SELECT 
                    PAC.nome_completo AS NomePaciente,
                    PAC.cpf AS CpfPaciente,
                    MED.nome_completo AS NomeMedico,
                    ESP.nome AS Especialidade,
                    DIS.data_disp AS DataConsulta,
                    DIS.hora_inicio AS HoraInicio,
                    DIS.hora_fim AS HoraFim,
                    MEDESP.valor_procedimento AS Preco
                FROM tb_pacientes PAC
                JOIN tb_consultas CON ON CON.cpf_paciente = PAC.cpf
                JOIN tb_disponibilidades DIS ON DIS.disponibilidade_id = CON.disponibilidade_id
                JOIN tb_medicos MED ON MED.cpf = DIS.cpf_medico
                JOIN tb_especialidades ESP ON ESP.especialidade_id = CON.especialidade_id
                JOIN tb_medico_especialidades MEDESP ON MEDESP.cpf_medico = MED.cpf AND MEDESP.especialidade_id = ESP.especialidade_id
                WHERE MED.nome_completo LIKE {0}
            "
            ;


            return ctx.Set<ConsultaDetalhadaDto>()
            .FromSqlRaw(sql, medicoNome)
            .ToList();
        }


        public List<Paciente> ListarTodos()
        {
            return ctx.Pacientes.ToList();
        }
    }
}

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
    public class EspecialidadeModel
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();

        public List<PaginaEspecialidadesDto> ListarInfoPaginaEspecialidades()
        {
            var sql = @"
                SELECT
                    ESP.nome AS 'especialidade',
	                ESP.descricao AS 'descricao',
	                COUNT(MED.cpf) 'numeroMedicos',
	                MIN(MEDESP.valor_procedimento) 'precoMinimo'
                FROM tb_especialidades ESP
                INNER JOIN tb_medico_especialidades MEDESP ON ESP.especialidade_id = MEDESP.especialidade_id
                INNER JOIN tb_medicos MED ON MED.cpf = MEDESP.cpf_medico
                GROUP BY ESP.nome, ESP.descricao";

            return ctx.Set<PaginaEspecialidadesDto>()
                .FromSqlRaw(sql)
                .ToList();
        }



        public List<Especialidade> ListarTodas()
        {
            return ctx.Especialidades.ToList();
        }
    }
}

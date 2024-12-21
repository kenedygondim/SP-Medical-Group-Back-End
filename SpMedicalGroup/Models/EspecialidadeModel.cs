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
        private readonly SpMedicalGroupContext ctx = new();

        public async Task<List<NomeEspecialidadeDto>> obterEspecialidadesMedico (string cpf)
        {
            var sql = @"
                SELECT ESP.nome FROM
                tb_especialidades ESP
                JOIN tb_medico_especialidades MEDESP ON MEDESP.especialidade_id = ESP.especialidade_id
                JOIN tb_medicos MED ON MED.cpf = MEDESP.cpf_medico
                WHERE MEDESP.cpf_medico = '" + cpf + "';";


            return await ctx.Set<NomeEspecialidadeDto>()
                .FromSqlRaw(sql)
                .ToListAsync();
        }


        public async Task<List<PaginaEspecialidadesDto>> ListarInfoPaginaEspecialidades()
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

            return await ctx.Set<PaginaEspecialidadesDto>()
                .FromSqlRaw(sql)
                .ToListAsync();
        }



        public async Task<List<Especialidade>> ListarTodas()
        {
            return await ctx.Especialidades.ToListAsync();
        }
    }
}

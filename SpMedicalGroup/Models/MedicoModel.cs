using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;


namespace SpMedicalGroup.Models
{
    public class MedicoModel
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();

        public List<MedicoInformacoesCardDto> ListarInformacoesBasicasMedico(string? especialidade, string? nomeMedico, string? numCrm)
        {
            int count = 0; // Certifique-se de inicializar o contador

            var sql = @"
                SELECT 
                    MED.cpf,
                    MED.nome_completo AS nomeCompleto, 
                    MED.crm,
                    FOT.foto_perfil_url AS fotoPerfilUrl,
                    EMP.nome_fantasia AS nomeFantasia
                FROM tb_medicos MED
                INNER JOIN tb_fotos_perfil FOT ON MED.foto_perfil_id = FOT.foto_perfil_id
                INNER JOIN tb_empresas EMP ON MED.cnpj_empresa = EMP.cnpj ";

            if (especialidade != null)
            {
                sql += @" 
                INNER JOIN tb_medico_especialidades MEDESP ON MED.cpf = MEDESP.cpf_medico
                INNER JOIN tb_especialidades ESP ON MEDESP.especialidade_id = ESP.especialidade_id
                WHERE ESP.nome LIKE '%" + especialidade + "%'";
                count++;
            }

            if (nomeMedico != null)
            {
                sql += (count > 0 ? " AND" : " WHERE") + " MED.nome_completo LIKE '%" + nomeMedico + "%'";
                count++;
            }

            if (numCrm != null)
            {
                sql += (count > 0 ? " AND" : " WHERE") + " MED.crm LIKE '%" + numCrm + "%'";
            }

            return ctx.Set<MedicoInformacoesCardDto>()
            .FromSqlRaw(sql)
            .ToList();
        }


        public List<Medico> ListarTodos()
        {
            return ctx.Medicos.ToList();
        }
    }
}

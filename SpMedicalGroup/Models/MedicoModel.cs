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
        private static readonly SpMedicalGroupContext ctx = new SpMedicalGroupContext();

        public async Task<InformacoesMedicoPopUp> InformacoesMedicoEspecifico(string cpfMedico)
        {
            var sql = @"SELECT 
	                         MED.cpf,
                             MED.nome_completo AS nomeCompleto, 
	                         MED.crm,
	                         MED.dt_nascimento AS dataNascimento,
                             USU.email,
                             FOT.foto_perfil_url AS fotoPerfilUrl,
                             EMP.nome_fantasia AS nomeFantasia,
	                         COUNT(CON.disponibilidade_id) AS numeroConsultas
                         FROM tb_medicos MED
                         JOIN tb_fotos_perfil FOT ON MED.foto_perfil_id = FOT.foto_perfil_id
                         JOIN tb_empresas EMP ON MED.cnpj_empresa = EMP.cnpj
                         JOIN tb_usuarios USU ON USU.usuario_id = MED.usuario_id
                         LEFT JOIN tb_disponibilidades DIS ON DIS.cpf_medico = MED.cpf
                         LEFT JOIN tb_consultas CON ON CON.disponibilidade_id = DIS.disponibilidade_id
                         GROUP BY MED.cpf, MED.nome_completo, MED.crm, MED.dt_nascimento, FOT.foto_perfil_url,  EMP.nome_fantasia, USU.email
                         HAVING MED.cpf = '" + cpfMedico + "'";

            return await ctx.Set<InformacoesMedicoPopUp>().FromSqlRaw(sql).FirstOrDefaultAsync();
        }


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

        public static async Task<string> buscaCpfMedicoPorEmail(string email)
        {
            try
            {
                var  cpfMedico = await
                (from tb_usuarios in ctx.Usuarios
                 join tb_medicos in ctx.Medicos
                 on tb_usuarios.UsuarioId equals tb_medicos.UsuarioId
                 where tb_usuarios.Email == email
                 select tb_medicos.Cpf).FirstOrDefaultAsync();

                return cpfMedico == null ? throw new Exception("Médico não encontrado") : cpfMedico;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
    }
}

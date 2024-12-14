using Microsoft.AspNetCore.Http.HttpResults;

namespace SpMedicalGroup.Domains
{
//    CREATE TABLE tb_medico_especialidades(
//    cpf_medico CHAR(11),
//	especialidade_id INT,
//    valor_procedimento DECIMAL(6,2)
//--  Definição de chaves primárias e estrangeiras:
//	PRIMARY KEY(cpf_medico, especialidade_id),
//	FOREIGN KEY(cpf_medico) REFERENCES tb_medicos(cpf),
//	FOREIGN KEY(especialidade_id) REFERENCES tb_especialidades(especialidade_id)
//)


    public class MedicoEspecialidade
    {
        public required string CpfMedico { get; set; }

        public virtual Medico Medico { get; set; }
        public required int EspecialidadeId { get; set; }
        public virtual Especialidade Especialidade { get; set; }
        public required decimal ValorProcedimento { get; set; }
    }
}
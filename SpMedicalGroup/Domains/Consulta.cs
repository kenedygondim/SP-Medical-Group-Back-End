using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Principal;
using System;

namespace SpMedicalGroup.Domains
{

//    CREATE TABLE tb_consultas(
//    consulta_id INT IDENTITY(1,1),
//	disponibilidade_id INT NOT NULL,
//    especialidade_id INT NOT NULL,
//	descricao VARCHAR(150) NOT NULL,
//    situacao VARCHAR(20) NOT NULL, --Criar ENUM na aplicação

//    cpf_paciente CHAR(11) NOT NULL,
//    is_telemedicina BIT NOT NULL, 
//--  Definição de chaves primárias e estrangeiras:
//	PRIMARY KEY(consulta_id),
//	FOREIGN KEY(disponibilidade_id) REFERENCES tb_disponibilidades(disponibilidade_id),
//	FOREIGN KEY(especialidade_id) REFERENCES tb_especialidades(especialidade_id),
//	FOREIGN KEY(cpf_paciente) REFERENCES tb_pacientes(cpf)
//)

    public class Consulta
    {
        public int ConsultaId { get; set; }
        public required int DisponibilidadeId { get; set; }
        public required int EspecialidadeId { get; set; }
        public required string Descricao { get; set; }
        public required string Situacao { get; set; }
        public required string CpfPaciente { get; set; }
        public required bool IsTelemedicina { get; set; }
    }
}

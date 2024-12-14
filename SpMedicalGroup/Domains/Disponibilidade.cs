using Microsoft.AspNetCore.Http.HttpResults;
using SpMedicalGroup.Domains;
using System.Security.Principal;

namespace SpMedicalGroup.Domains
{
//    CREATE TABLE tb_disponibilidades(
//        disponibilidade_id INT IDENTITY(1,1),
//	cpf_medico CHAR(11) NOT NULL,
//    data_disp CHAR(10) NOT NULL,
//    hora_inicio CHAR(5) NOT NULL,
//    hora_fim CHAR(5) NOT NULL,
//	--  Definição de chaves primárias e estrangeiras:
//	PRIMARY KEY(disponibilidade_id),
//	FOREIGN KEY(cpf_medico) REFERENCES tb_medicos(cpf),
//)

    public class Disponibilidade
    {
        public int DisponibilidadeId { get; set; }
        public required string CpfMedico { get; set; }
        public required string DataDisp { get; set; }
        public required string HoraInicio { get; set; }
        public required string HoraFim { get; set; }

        public Medico Medico { get; set; }
    }
}


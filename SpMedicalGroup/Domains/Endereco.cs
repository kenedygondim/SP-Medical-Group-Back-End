using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;

namespace SpMedicalGroup.Domains
{

//    CREATE TABLE tb_enderecos(
//    endereco_id INT IDENTITY(1,1),
//	cep CHAR(8) NOT NULL,
//    uf CHAR(2) NOT NULL,
//    municipio VARCHAR(35) NOT NULL,
//    bairro VARCHAR(105) NOT NULL,
//    numero VARCHAR(10) NOT NULL,
//    complemento VARCHAR(10),
//	--  Definição de chaves primárias e estrangeiras:
//	PRIMARY KEY(endereco_id)
//)

    public class Endereco
    {
        public int EnderecoId { get; set; }
        public required string Cep { get; set; }
        public required string Uf { get; set; }
        public required string Municipio { get; set; }
        public required string Bairro { get; set; }
        public required string Logradouro { get; set; }
        public required string Numero { get; set; }
        public string? Complemento { get; set; }
    }
}
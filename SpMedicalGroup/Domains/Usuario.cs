using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Principal;

namespace SpMedicalGroup.Domains
{

//    CREATE TABLE tb_usuarios(
//    usuario_id INT IDENTITY(1,1),
//	role_id INT NOT NULL,
//    email VARCHAR(60) NOT NULL UNIQUE,
//	senha VARCHAR(150) NOT NULL,
//--  Definição de chaves primárias e estrangeiras:
//	PRIMARY KEY(usuario_id),
//	FOREIGN KEY(role_id) REFERENCES tb_roles(role_id)
//)

    public class Usuario
    {
        public int UsuarioId { get; set; }
        public required int RoleId { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}

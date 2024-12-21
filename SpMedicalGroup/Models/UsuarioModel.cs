using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;


namespace SpMedicalGroup.Models
{
    public class UsuarioModel
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();

        public List<Usuario> ListarTodos()
        {
            return ctx.Usuarios.ToList();
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {
            try
            {
                Log.Information("Cadastrando usuário...");
                await ctx.Usuarios.AddAsync(usuario);
                await ctx.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


            public Usuario Login(string email, string senha)
        {
            Log.Information("Buscando usuário...");
            return ctx.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}

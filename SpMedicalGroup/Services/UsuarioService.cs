using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;


namespace SpMedicalGroup.Services
{
    public class UsuarioService
    {
        private readonly SpMedicalGroupContext ctx = new();

        public async Task<List<Usuario>> ListarTodos()
        {
            return await ctx.Usuarios.ToListAsync();
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {

            await ctx.Usuarios.AddAsync(usuario);
            await ctx.SaveChangesAsync();
            return usuario;
        }


        public async Task<Usuario> Login(string email, string senha)
        {
            return await ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha) ?? throw new Exception("Usuário não encontrado.");
        }
    }
}

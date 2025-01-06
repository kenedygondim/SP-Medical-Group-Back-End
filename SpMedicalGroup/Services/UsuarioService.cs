using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SpMedicalGroupContext ctx;

        public UsuarioService(SpMedicalGroupContext ctx)
        {
            this.ctx = ctx;
        }

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

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("Usuário não encontrado.");
        }
    }
}

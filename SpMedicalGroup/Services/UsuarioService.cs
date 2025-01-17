using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;
using BCrypt.Net;


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
            string senhaCriptografada = CriptografarSenha(usuario.Senha);
            usuario.Senha = senhaCriptografada;
            await ctx.Usuarios.AddAsync(usuario);
            await ctx.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Login(string email, string senha)
        {
            Usuario usuario = await GetUsuarioByEmail(email);

            if (!VerificarSenha(senha, usuario.Senha))
                throw new Exception("Senha incorreta");

            return usuario;
         }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("Usuário não encontrado.");
        }

        public string CriptografarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senhaForncecida, string senhaArmazenada)
        {
            return BCrypt.Net.BCrypt.Verify(senhaForncecida, senhaArmazenada);  
        }
    }
}

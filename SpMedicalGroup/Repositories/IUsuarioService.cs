using Amazon.S3.Encryption.Internal;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ListarTodos();
        Task<Usuario> CadastrarUsuario(Usuario usuario);
        Task<Usuario> Login(string email, string senha);
        Task<Usuario> GetUsuarioByEmail(string email);
        string CriptografarSenha(string senha);
        bool VerificarSenha(string senhaFornecida, string senhaArmazenada);
    }
}

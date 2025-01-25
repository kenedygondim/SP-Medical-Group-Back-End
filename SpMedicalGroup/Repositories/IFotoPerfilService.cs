using SpMedicalGroup.Dto.FotoPerfil;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IFotoPerfilService
    {
        Task<FotoPerfil> AdicionarFotoPerfil(FotoPerfil fotoPerfil);
        Task<string> AlterarFotoPerfil(string email, IFormFile novaFotoPerfil);
    }
}

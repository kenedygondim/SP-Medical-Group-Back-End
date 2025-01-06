using SpMedicalGroup.Dto.FotoPerfil;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Repositories
{
    public interface IFotoPerfilService
    {
        Task<FotoPerfil> CadastrarFotoPerfil(FotoPerfil fotoPerfil);
        Task<string> AlterarFotoPerfil(AlterarFotoPerfilDto novaFotoPerfil);
    }
}

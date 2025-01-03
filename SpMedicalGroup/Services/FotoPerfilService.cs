using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Services
{
    public class FotoPerfilService
    {
        private readonly SpMedicalGroupContext ctx = new();

        public async Task<FotoPerfil> cadastrarFotoPerfil(FotoPerfil fotoPerfil)
        {
            await ctx.FotosPerfil.AddAsync(fotoPerfil);
            await ctx.SaveChangesAsync();
            return fotoPerfil;
        }
    }
}

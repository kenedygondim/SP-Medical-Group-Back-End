using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Dto.FotoPerfil;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;
using SpMedicalGroup.Services.AWS;
using System.IO;

namespace SpMedicalGroup.Services
{
    public class FotoPerfilService : IFotoPerfilService
    {
        private readonly SpMedicalGroupContext ctx;
        private readonly S3Service s3Service;

        public FotoPerfilService(SpMedicalGroupContext ctx, S3Service s3Service)
        {
            this.ctx = ctx;
            this.s3Service = s3Service;
        }


        public async Task<FotoPerfil> CadastrarFotoPerfil(FotoPerfil fotoPerfil)
        {
            await ctx.FotosPerfil.AddAsync(fotoPerfil);
            await ctx.SaveChangesAsync();
            return fotoPerfil;
        }

        public async Task<string> AlterarFotoPerfil(AlterarFotoPerfilDto novaFotoPerfil)
        {

            Console.WriteLine(novaFotoPerfil.ToString());

            var usuario = await ctx.Usuarios.Where(u => u.Email == novaFotoPerfil.Email).FirstOrDefaultAsync() ?? throw new Exception(novaFotoPerfil.Email.ToString());

            if (novaFotoPerfil.FotoPerfil == null)
                throw new Exception("Foto de perfil não informada.");

            var fileName = $"SP-MEDICAL-GROUP-USER-PROFILE-PICTURE-{usuario.UsuarioId}";
            using var stream = novaFotoPerfil.FotoPerfil.OpenReadStream();
            string urlAws = await s3Service.UploadFileAsync(stream, fileName);

            var paciente = await ctx.Pacientes.Where(p => p.UsuarioId == usuario.UsuarioId).FirstOrDefaultAsync() ?? throw new Exception("Paciente não encontrado");
            var fotoPerfil = await ctx.FotosPerfil.Where(f => f.FotoPerfilId == paciente.FotoPerfilId).FirstOrDefaultAsync() ?? throw new Exception("Foto de perfil não encontrada" + paciente.ToString());


            fotoPerfil.FotoPerfilUrl = urlAws;

            await ctx.SaveChangesAsync();

            return "Foto de perfil alterada com sucesso.";
        }

    }
}

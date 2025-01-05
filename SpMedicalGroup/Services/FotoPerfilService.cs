using SpMedicalGroup.Contexts;
using SpMedicalGroup.Dto.FotoPerfil;
using SpMedicalGroup.Models;
using SpMedicalGroup.Services.AWS;
using System.IO;

namespace SpMedicalGroup.Services
{
    public class FotoPerfilService
    {
        private readonly SpMedicalGroupContext ctx = new();
        private readonly S3Service s3Service = new();
        private readonly PacienteService pacienteService = new();

        public async Task<FotoPerfil> CadastrarFotoPerfil(FotoPerfil fotoPerfil)
        {
            await ctx.FotosPerfil.AddAsync(fotoPerfil);
            await ctx.SaveChangesAsync();
            return fotoPerfil;
        }

        public async Task<string> AlterarFotoPerfil(AlterarFotoPerfilDto novaFotoPerfil)
        {
            Paciente paciente = await pacienteService.GetPacienteByEmail(novaFotoPerfil.Email);

            if (novaFotoPerfil.FotoPerfil == null)
                throw new Exception("Foto de perfil não informada.");

            var fileName = $"SP-MEDICAL-GROUP-USER-PROFILE-PICTURE-{paciente.UsuarioId}";
            using var stream = novaFotoPerfil.FotoPerfil.OpenReadStream();
            string urlAws = await s3Service.UploadFileAsync(stream, fileName);
            paciente.FotoPerfil.FotoPerfilUrl = urlAws;

            await ctx.SaveChangesAsync();

            return "Foto de perfil alterada com sucesso.";
        }

    }
}

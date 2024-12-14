using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Principal;

namespace SpMedicalGroup.Domains
{
    public class FotoPerfil
    {
        public int FotoPerfilId { get; set; }
        public string? FotoPerfilUrl { get; set; }
    }
}
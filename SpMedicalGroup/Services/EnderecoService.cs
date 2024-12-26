using Serilog;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Services
{
    public class EnderecoService
    {
        private readonly SpMedicalGroupContext ctx = new();

        public async Task<Endereco> CadastrarEndereco(Endereco endereco)
        {
            await ctx.Enderecos.AddAsync(endereco);
            await ctx.SaveChangesAsync();
            return endereco;
        }
    }
}

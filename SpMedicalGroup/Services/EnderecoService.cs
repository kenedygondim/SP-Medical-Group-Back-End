using Serilog;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;

namespace SpMedicalGroup.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly SpMedicalGroupContext ctx;

        public EnderecoService(SpMedicalGroupContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Endereco> CadastrarEndereco(Endereco endereco)
        {
            await ctx.Enderecos.AddAsync(endereco);
            await ctx.SaveChangesAsync();
            return endereco;
        }
    }
}

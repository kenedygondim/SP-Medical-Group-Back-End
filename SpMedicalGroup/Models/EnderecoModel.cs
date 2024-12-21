using Serilog;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;

namespace SpMedicalGroup.Models
{
    public class EnderecoModel
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();

        public async Task<Endereco> CadastrarEndereco(Endereco endereco)
        {
            try
            {
                Log.Information("Cadastrando endereço...");
                await ctx.Enderecos.AddAsync(endereco);
                await ctx.SaveChangesAsync();
                return endereco;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

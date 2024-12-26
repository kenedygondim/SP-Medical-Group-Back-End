using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Services
{
    public class EmpresaService
    {
        private readonly SpMedicalGroupContext ctx = new();
        public async Task<Empresa> Cadastrar(Empresa novaEmpresa)
        {
            await ctx.Empresas.AddAsync(novaEmpresa);
            await ctx.SaveChangesAsync();

            return novaEmpresa;
        }

        public async Task<List<Empresa>> ListarTodas()
        {
            return await ctx.Empresas.ToListAsync();
        }
    }
}

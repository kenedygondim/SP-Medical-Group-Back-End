using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;

namespace SpMedicalGroup.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly SpMedicalGroupContext ctx;

        public EmpresaService(SpMedicalGroupContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Empresa> CadastrarEmpresa(Empresa novaEmpresa)
        {
            await ctx.Empresas.AddAsync(novaEmpresa);
            await ctx.SaveChangesAsync();

            return novaEmpresa;
        }

        public async Task<List<Empresa>> GetAllEmpresas()
        {
            return await ctx.Empresas.ToListAsync();
        }
    }
}

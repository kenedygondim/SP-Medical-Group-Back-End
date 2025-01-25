using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Dto.Especialidade;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;

namespace SpMedicalGroup.Services
{
    public class EspecialidadeService : IEspecialidadeService
    {
        private readonly SpMedicalGroupContext ctx;

        public EspecialidadeService(SpMedicalGroupContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<IdNomeEspecialidadeDto>> GetAllEspecialidadesMedico(string cpf)
        {
            var especialidadesMedico = await 
                (from esp in ctx.Especialidades
                join medEsp in ctx.MedicosEspecialidades on esp.EspecialidadeId equals medEsp.EspecialidadeId
                join med in ctx.Medicos on medEsp.CpfMedico equals med.Cpf
                where medEsp.CpfMedico == cpf
                select new IdNomeEspecialidadeDto
                {
                    EspecialidadeId = esp.EspecialidadeId,
                    Nome = esp.Nome
                }).ToListAsync();

            return especialidadesMedico;
        }

        public async Task<List<PaginaEspecialidadesDto>> GetDetalhesEspecialidades()
        {
            List<PaginaEspecialidadesDto> paginaEspecialidadesDtos = await 
                ctx.Set<PaginaEspecialidadesDto>().FromSqlRaw("SELECT * FROM Especialidades_Cards").OrderBy(a => a.Especialidade).ToListAsync();

            return paginaEspecialidadesDtos;
        }


        public async Task<List<Especialidade>> GetAllEspecialidades()
        {
            return await ctx.Especialidades.ToListAsync();
        }
    }
}

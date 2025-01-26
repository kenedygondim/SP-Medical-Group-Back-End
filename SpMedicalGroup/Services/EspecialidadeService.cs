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

        public async Task<string> ExcluirEspecialidade(int idEspecialidade)
        {
            Especialidade especialidadeEncontrada = await ctx.Especialidades
                .Where(e => e.EspecialidadeId == idEspecialidade).FirstOrDefaultAsync()
                ?? throw new Exception("Especialidade não encontrada");

            bool especialidadePossuiConsultas = await ctx.Consulta.FirstOrDefaultAsync(c => c.EspecialidadeId == idEspecialidade) != null;

            if (especialidadePossuiConsultas)
            {
                throw new Exception("Não é possível excluir uma especialidade que possui consultas.");
            }

            ctx.Especialidades.Remove(especialidadeEncontrada);
            await ctx.SaveChangesAsync();

            return "Especialidade excluída com sucesso.";
        }

        public async Task<AdicionarEspecialidadeDto> AdicionarEspecialidade(AdicionarEspecialidadeDto adicionarEspecialidadeDto)
        {
            if (adicionarEspecialidadeDto.NomeEspecialidade is null)
                throw new Exception("Campo chegou nulo no service");


            bool especialidadeEncontrada = await ctx.Especialidades
                .Where(e => e.Nome == adicionarEspecialidadeDto.NomeEspecialidade).FirstOrDefaultAsync() != null;

            if (especialidadeEncontrada)
            {
                throw new Exception("Já existe uma especialidade com o mesmo nome!");
            }

            await ctx.Especialidades.AddAsync(new Especialidade { Nome = adicionarEspecialidadeDto.NomeEspecialidade, Descricao = adicionarEspecialidadeDto.DescricaoEspecialidade });
            await ctx.SaveChangesAsync();

            return adicionarEspecialidadeDto;
        }
    }
}

using EsercitazioneAspNet.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EsercitazioneAspNet.Repositories
{
    public class BancaDbRepository : IBancaDbRepository
    {

        private SoluzioneBankomatContext _ctx;


        public BancaDbRepository(SoluzioneBankomatContext ctx)
        {
            _ctx = ctx ?? throw new AggregateException(nameof(ctx));
        }



        public async Task<IEnumerable<Banche>> GetBancheAsync()
        {
            return await _ctx.Banches.OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<Banche?> GetBancaAsync(int bancheId)
        {
            return await _ctx.Banches.Include(b => b.BancheFunzionalita).ThenInclude(f => f.IdFunzionalitaNavigation)
            .Where(c => c.Id == bancheId)
            .FirstOrDefaultAsync();
        }

        public async Task<List<Funzionalitum>> GetFunzionalitaAsync(int bancheId)
        {
            var Banca = await GetBancaAsync(bancheId);
            if (Banca == null)
            {
                return null;
            }
            return await _ctx.Funzionalita.Where(x => x.BancheFunzionalita.Any(y => y.IdBanca ==  bancheId)).ToListAsync();
        }
        


    }
}

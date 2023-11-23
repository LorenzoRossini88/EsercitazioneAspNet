using EsercitazioneAspNet.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;

namespace EsercitazioneAspNet.Repositories
{
    public class FunzionalitaDbRepository : IFunzionalitaDbRepository
    {
        private SoluzioneBankomatContext _ctx;
        


        public FunzionalitaDbRepository(SoluzioneBankomatContext ctx)
        {
            _ctx = ctx ?? throw new AggregateException(nameof(ctx));
            
        }


        public async Task<IEnumerable<Funzionalitum>> GetFunzionalitaAsync()
        {
            return await _ctx.Funzionalita.OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<Funzionalitum?> GetFunzionalitaByIdAsync(int bancaId)
        {
            return await _ctx.Funzionalita
            .Where(c => c.Id== bancaId)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Models.BancheFunzionalitum>> GetFunzionalitaByBancaIdAsync(int bancaId)
        {
            // Sostituisci questo con la tua logica di accesso ai dati
            var funzionalitaList = await _ctx.BancheFunzionalita
                .Where(bf => bf.IdBanca == bancaId)
                .ToListAsync();

            return funzionalitaList;
        }


        public async Task<IEnumerable<string>> GetFunzionalitaNomiByIdsAsync(IEnumerable<int> funzionalitaIds)
        {
            var funzionalitaNomi = await _ctx.Funzionalita
                .Where(funz => funzionalitaIds.Contains((int)funz.Id))
                .Select(funz => funz.Nome)
                .ToListAsync();

            return funzionalitaNomi;
        }

        public async Task<IEnumerable<Funzionalitum>> GetFunzionalitaBancaAsync(int bancaId)
        {
            var banca = await GetBancaAsync(bancaId);
            if (banca == null)
            {
                return null;
            }
            return await _ctx.Funzionalita.Where(x => x.BancheFunzionalita.Where(y => y.IdBanca == bancaId).Count()>0).ToListAsync();
        }


        public async Task<Banche?> GetBancaAsync(int bancheId)
        {
            return await _ctx.Banches.Include(b => b.BancheFunzionalita).ThenInclude(f => f.IdFunzionalitaNavigation)
            .Where(c => c.Id == bancheId)
            .FirstOrDefaultAsync();
        }




    }
}

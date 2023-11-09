using EsercitazioneAspNet.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}

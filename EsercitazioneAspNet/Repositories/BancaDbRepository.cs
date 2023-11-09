using EsercitazioneAspNet.Models;
using Microsoft.EntityFrameworkCore;

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

        //public async Task<Funzionalitum> GetFunzionalitaAsync(int bancheId)
        //{
        //    var banca = await _ctx.Banches
        //    .Where(c => c.Id == bancheId)
        //    .FirstOrDefaultAsync();

        //    banca.BancheFunzionalita
        //}


    }
}

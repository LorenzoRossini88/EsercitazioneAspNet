using EsercitazioneAspNet.Models;

namespace EsercitazioneAspNet.Repositories
{
    public interface IFunzionalitaDbRepository
    {
        Task<IEnumerable<Funzionalitum>> GetFunzionalitaAsync();

        Task<Funzionalitum?> GetFunzionalitaByIdAsync(int bancaId);
        Task<IEnumerable<Models.BancheFunzionalitum>> GetFunzionalitaByBancaIdAsync(int bancaId);

        Task<IEnumerable<string>> GetFunzionalitaNomiByIdsAsync(IEnumerable<int> funzionalitaIds);

        Task<IEnumerable<Funzionalitum>> GetFunzionalitaBancaAsync(int bancaId);
        Task<Banche?> GetBancaAsync(int bancheId);
    }
}

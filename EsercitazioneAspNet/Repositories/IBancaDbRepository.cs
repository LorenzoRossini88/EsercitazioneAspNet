using EsercitazioneAspNet.Models;

namespace EsercitazioneAspNet.Repositories
{
    public interface IBancaDbRepository
    {
        Task<IEnumerable<Banche>> GetBancheAsync();

        Task<Banche?> GetBancaAsync(int bancaId);
    }
}

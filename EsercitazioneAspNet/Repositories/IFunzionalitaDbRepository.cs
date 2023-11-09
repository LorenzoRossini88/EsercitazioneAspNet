using EsercitazioneAspNet.Models;

namespace EsercitazioneAspNet.Repositories
{
    public interface IFunzionalitaDbRepository
    {
        Task<IEnumerable<Funzionalitum>> GetFunzionalitaAsync();

        Task<Funzionalitum?> GetFunzionalitaByIdAsync(int bancaId);
    }
}

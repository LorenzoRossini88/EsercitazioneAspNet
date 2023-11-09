using EsercitazioneAspNet.Dto;
using EsercitazioneAspNet.Models;
using System.ComponentModel;


namespace EsercitazioneAspNet.Repositories
{
    public interface IUtentiDbRepository
    {
        Task<IEnumerable<Utenti>> GetUtentiAsync();

        Task<Utenti?> GetUtenteAsync(int utentiId);

        Task AddUtenteAsync(Utenti utente);

        Task UpdateUtenteAsync(Utenti updatedUtente);

        Task DeleteUtenteAsync(Utenti deletedUtente);

    }
}

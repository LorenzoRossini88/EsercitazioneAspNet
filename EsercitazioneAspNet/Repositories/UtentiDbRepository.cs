using EsercitazioneAspNet.Models;
using System;
using Microsoft.EntityFrameworkCore; 
using EsercitazioneAspNet.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneAspNet.Repositories
{
    public class UtentiDbRepository : IUtentiDbRepository
    {

        private SoluzioneBankomatContext _ctx;


        public UtentiDbRepository(SoluzioneBankomatContext ctx)
        {
            _ctx = ctx ?? throw new AggregateException(nameof(ctx));
        }

        

        public async Task<IEnumerable<Utenti>> GetUtentiAsync()
        {
            return await _ctx.Utentis.OrderBy(c => c.NomeUtente).ToListAsync();
        }

        public async Task<Utenti?> GetUtenteAsync(int utentiId)
        {
            return await _ctx.Utentis
            .Where(c => c.Id == utentiId)
            .FirstOrDefaultAsync();
        }

        public async Task AddUtenteAsync(Utenti utente)
        {
            if (utente != null)
            {
                _ctx.Utentis.Add(utente);
                await _ctx.SaveChangesAsync();
                
            }
            
        }

        public async Task UpdateUtenteAsync(Utenti updatedUtente)
        {
            if (updatedUtente != null)
            {
                var existingUtente = await _ctx.Utentis.FindAsync(updatedUtente.Id);

                if (existingUtente != null)
                {
                    existingUtente.Bloccato = updatedUtente.Bloccato;
                    existingUtente.Password = updatedUtente.Password;

                    await _ctx.SaveChangesAsync();
                }
            }
        }


        public async Task DeleteUtenteAsync(Utenti utenti)
        {
            if (utenti != null)
            {
                _ctx.Utentis.Remove(utenti);
                await _ctx.SaveChangesAsync();
            }
        }



        
    }
}

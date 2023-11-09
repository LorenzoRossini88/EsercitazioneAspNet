using Microsoft.AspNetCore.Mvc;
using EsercitazioneAspNet.Repositories;
using EsercitazioneAspNet.Models;
using EsercitazioneAspNet.Dto;
using AutoMapper;

namespace EsercitazioneAspNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {

        private readonly IUtentiDbRepository _utentiRepo;
        private readonly IMapper _mapper;


        public UtenteController(IUtentiDbRepository utentiRepo, IMapper mapper)
        {
            _utentiRepo = utentiRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Utenti>>> GetUtenti()
        {
            

            IEnumerable<Models.Utenti> utentis = await _utentiRepo.GetUtentiAsync();
            return Ok(utentis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Utenti>> GetUtente(int id)
        {
            var UtenteToReturn = (await _utentiRepo.GetUtentiAsync()).FirstOrDefault(c => c.Id == id);

            if (UtenteToReturn == null)
            {
                return NotFound();
            }
            return Ok(UtenteToReturn);
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUtentiAsync(int id, [FromBody] Utenti updatedUtente)
        //{
        //    if (updatedUtente == null)
        //    {
        //        return BadRequest("dati utente non validi");
        //    }

        //    var existingUtente = await _utentiRepo.GetUtenteAsync(id);

        //    if (existingUtente == null)
        //    {
        //        return NotFound("utente non trovato");
        //    }

        //    existingUtente.Bloccato = updatedUtente.Bloccato;
        //    existingUtente.Password = updatedUtente.Password;


        //    await _utentiRepo.UpdateUtenteAsync(existingUtente);

        //    return Ok("utente aggiornato con successo");
        //}


        [HttpPut("update-bloccato/{id}")]
        public async Task<IActionResult> PutUtentiAsync(int id, [FromBody] bool nuovoStatoBloccato)
        {
            var existingUtente = await _utentiRepo.GetUtenteAsync(id);

            if (existingUtente == null)
            {
                return NotFound("Utente non trovato");
            }

            
            existingUtente.Bloccato = nuovoStatoBloccato;

            await _utentiRepo.UpdateUtenteAsync(existingUtente);

            return Ok("Utente aggiornato con successo");
        }



        [HttpPut("update-password/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] string nuovaPassword)
        {
            var existingUtente = await _utentiRepo.GetUtenteAsync(id);

            if (existingUtente == null)
            {
                return NotFound("Utente non trovato");
            }

           
            if (string.IsNullOrWhiteSpace(nuovaPassword))
            {
                return BadRequest("La nuova password non è valida");
            }

            
            existingUtente.Password = nuovaPassword;

            await _utentiRepo.UpdateUtenteAsync(existingUtente);

            return Ok("Password dell'utente aggiornata con successo");
        }


        [HttpPost]
        public async Task<IActionResult> PostUtenteAsync([FromBody] UtentiDto nuovoUtente)
        {
            if (ModelState.IsValid)
            {
                //var ris = await _utentiRepo.AddUtenteAsync();

                var ris = _mapper.Map<Utenti>(nuovoUtente);
                ris.ContiCorrentes = new List<ContiCorrente> { new ContiCorrente { Saldo = 0 } };
                
                await _utentiRepo.AddUtenteAsync(ris);

                return Ok("aggiunto con successo utente con id: " + ris);
            }
            return BadRequest(ModelState);


            //var utente = _mapper.Map<List<Utenti>>(await _utentiRepo.GetUtentiAsync());

            //nuovoUtente.Id = utente.Count() + 1;
            //await _utentiRepo.AddUtenteAsync(nuovoUtente);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtenteAsync(int id)
        {
            var ut = await _utentiRepo.GetUtentiAsync();
            var utente = ut.FirstOrDefault(c => c.Id == id);

            if (utente == null)
            {
                return NotFound("utente non trovata");
            }

            await _utentiRepo.DeleteUtenteAsync(utente);

            var res = _mapper.Map<List<UtentiDto>>(await _utentiRepo.GetUtentiAsync());

            return Ok("utente eliminato con successo");
        }







    }
}

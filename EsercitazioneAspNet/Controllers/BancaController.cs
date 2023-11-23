using EsercitazioneAspNet.Models;
using EsercitazioneAspNet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancaController : ControllerBase
    {
        private readonly IBancaDbRepository _bancaRepo;


        public BancaController(IBancaDbRepository bancaRepo)
        {
            _bancaRepo = bancaRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Banche>>> GetBanche()
        {


            IEnumerable<Models.Banche> banches = await _bancaRepo.GetBancheAsync();
            return Ok(banches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Banche>> GetBanca(int id)
        {
            var BancaToReturn = (await _bancaRepo.GetBancheAsync()).FirstOrDefault(c => c.Id == id);

            if (BancaToReturn == null)
            {
                return NotFound();
            }
            return Ok(BancaToReturn);
        }



        [HttpGet("{bancaId}/funzionalita")]
        public async Task<ActionResult<IEnumerable<Funzionalitum>>> GetFunzionalitaBanca(int bancaId)
        {
            try
            {
                var funz = await _bancaRepo.GetFunzionalitaAsync(bancaId);

                if (funz == null)
                {
                    return NotFound("Banca non trovata");
                }

                

                return Ok(funz);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Errore durante la richiesta delle funzionalità: {ex.Message}");
            }
        }





    }
}

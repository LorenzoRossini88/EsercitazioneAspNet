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
            var banca = await _bancaRepo.GetBancaAsync(bancaId);

            if (banca == null)
            {
                return NotFound("Banca non trovata");
            }

            var funzionalita = banca.BancheFunzionalita;

            return Ok(funzionalita);
        }



    }
}

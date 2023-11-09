using EsercitazioneAspNet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneAspNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FunzionalitaController : ControllerBase
    {
        private readonly IFunzionalitaDbRepository _funzRepo;


        public FunzionalitaController(IFunzionalitaDbRepository funzRepo)
        {
            _funzRepo = funzRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Funzionalitum>>> GetFunzionalita()
        {


            IEnumerable<Models.Funzionalitum> funzionalita = await _funzRepo.GetFunzionalitaAsync();
            return Ok(funzionalita);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Funzionalitum>> GetFunzionalitaById(int bancaId)
        {
            var FunzToReturn = (await _funzRepo.GetFunzionalitaByIdAsync(bancaId));

            if (FunzToReturn == null)
            {
                return NotFound();
            }
            return Ok(FunzToReturn.Nome);
        }
    }
}

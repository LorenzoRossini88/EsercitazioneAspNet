using EsercitazioneAspNet.Dto;
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


        [HttpGet("funz-banche/{bancaId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetFunzionalitasByBancaId(int bancaId)
        {
            var funzionalitaList = await _funzRepo.GetFunzionalitaByBancaIdAsync(bancaId);

            if (funzionalitaList == null || !funzionalitaList.Any())
            {
                return NotFound();
            }

            // Converti gli ID delle funzionalità da long a int
            var funzionalitaIds = funzionalitaList
                .Select(bf => Convert.ToInt32(bf.IdFunzionalita))
                .ToList();

            // Ottieni i nomi delle funzionalità corrispondenti agli ID
            var funzionalitaNomi = await _funzRepo.GetFunzionalitaNomiByIdsAsync(funzionalitaIds);

            return Ok(funzionalitaNomi);
        }


        //[HttpGet("funz-banche/{bancaId}")]
        //public async Task<ActionResult<IEnumerable<FunzionalitaInfo>>> GetFunzionalitasByBancaId(int bancaId)
        //{
        //    var funzionalitaList = await _funzRepo.GetFunzionalitaByBancaIdAsync(bancaId);

        //    if (funzionalitaList == null || !funzionalitaList.Any())
        //    {
        //        return NotFound();
        //    }

        //    // Converti gli ID delle funzionalità da long a int
        //    var funzionalitaInfos = funzionalitaList
        //        .Select(bf => new FunzionalitaInfo
        //        {
        //            Id = (int)bf.IdFunzionalita,
        //            Nome = bf.Nome
        //        })
        //        .ToList();

        //    return Ok(funzionalitaInfos);
        //}









    }
}

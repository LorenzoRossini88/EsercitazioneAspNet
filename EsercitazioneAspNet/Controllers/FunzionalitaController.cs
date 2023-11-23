using AutoMapper;
using EsercitazioneAspNet.Dto;
using EsercitazioneAspNet.Models;
using EsercitazioneAspNet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneAspNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FunzionalitaController : ControllerBase
    {
        private readonly IFunzionalitaDbRepository _funzRepo;
        private readonly IMapper _mapper;


        public FunzionalitaController(IFunzionalitaDbRepository funzRepo, IMapper mapper)
        {
            _funzRepo = funzRepo;
            _mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<BancheFunzionalitum>>> GetFunzionalitasByBancaId(int bancaId)
        {
            var funzionalitaList = await _funzRepo.GetFunzionalitaBancaAsync(bancaId);

            if (funzionalitaList == null || !funzionalitaList.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Funzionalitum>, IEnumerable<FunzionalitaDto>>(funzionalitaList));
            
        }


        //[HttpGet("funz-banche/{bancaId}")]
        //public async Task<ActionResult<IEnumerable<FunzionalitaInfo>>> GetFunzionalitasByBancaId(int bancaId)
        //{
        //    var funzionalitaList = await _funzRepo.GetFunzionalitaByBancaIdAsync(bancaId);

        //    if (funzionalitaList == null || !funzionalitaList.Any())
        //    {
        //        return NotFound();
        //    }

        //    // Converto gli ID delle funzionalità da long a int
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

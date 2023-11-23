using AutoMapper;
using EsercitazioneAspNet.Models;
using EsercitazioneAspNet.Dto;

namespace EsercitazioneAspNet.Profiles
{
    public class FunzionalitaProfile: Profile
    {
        public FunzionalitaProfile()
        {
            CreateMap<Funzionalitum, FunzionalitaDto>().ReverseMap();
        }
    }
}

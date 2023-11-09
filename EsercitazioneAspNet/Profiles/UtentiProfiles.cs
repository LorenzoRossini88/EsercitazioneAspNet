using AutoMapper;
using EsercitazioneAspNet.Models;
using EsercitazioneAspNet.Dto;

namespace EsercitazioneAspNet.Profiles
{
    public class UtentiProfiles : Profile
    {

        public UtentiProfiles()
        {
            CreateMap<Utenti, UtentiDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using MovieApi.Dtos.Actors;
using MovieApi.Entities;

namespace MovieApi.Mappers
{
    public class ActorMapperProfile : Profile
    {
        public ActorMapperProfile()
        {
            CreateMap<ActorDto, Actor>().ReverseMap();
            CreateMap<CreateActorDto, Actor>();
            CreateMap<UpdateActorDto, Actor>();
        }
    }
}

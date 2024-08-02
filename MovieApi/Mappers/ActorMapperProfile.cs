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

            CreateMap<CreateActorDto, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());

            CreateMap<UpdateActorDto, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());


            CreateMap<PatchActorDto,Actor>().ReverseMap();

        }
    }
}

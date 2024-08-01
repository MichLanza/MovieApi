using AutoMapper;
using MovieApi.Dtos.Genre;
using MovieApi.Entities;

namespace MovieApi.Mappers
{
    public class GenreMapperProfile : Profile
    {
        public GenreMapperProfile()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();

            CreateMap<CreateGenreDto, Genre>();

            CreateMap<UpdateGenreDto, Genre>();

        }
    }
}

using AutoMapper;
using MovieApi.Dtos.Movies;
using MovieApi.Dtos.NewFolder;
using MovieApi.Entities;

namespace MovieApi.Mappers
{
    public class MovieMapperProfile : Profile
    {
        public MovieMapperProfile()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<CreateMovieDto, Movie>()
                 .ForMember(x => x.Poster, options => options.Ignore());

            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore());


            CreateMap<PatchMovieDto, Movie>().ReverseMap();
        }
    }
}

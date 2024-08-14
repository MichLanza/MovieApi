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
                 .ForMember(x => x.Poster, options => options.Ignore())
                 .ForMember(x => x.MovieGenre, options => options.MapFrom(MapMovieGenres))
                 .ForMember(x => x.MovieActors, options => options.MapFrom(MapMovieActors));


            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MovieGenre, options => options.MapFrom(MapMovieGenres))
                .ForMember(x => x.MovieActors, options => options.MapFrom(MapMovieActors));


            CreateMap<PatchMovieDto, Movie>().ReverseMap();
        }

        private List<MovieActor> MapMovieActors(CreateMovieDto dto, Movie movie)
        {
            var result = new List<MovieActor>();
            if (dto.Actors is null) { return result; }

            foreach (var actor in dto.Actors)
            {
                result.Add(new MovieActor()
                {
                    ActorId = actor.ActorId,
                    Character = actor.Character
                });
            }
            return result;
        }

        private List<MovieGenre> MapMovieGenres(CreateMovieDto dto, Movie movie)
        {
            var result = new List<MovieGenre>();
            if (dto.GenreIds is null) { return result; }

            foreach (var id in dto.GenreIds)
            {
                result.Add(new MovieGenre() { GenreId = id });
            }
            return result;
        }


        private List<MovieActor> MapMovieActors(UpdateMovieDto dto, Movie movie)
        {
            var result = new List<MovieActor>();
            if (dto.Actors is null) { return result; }

            foreach (var actor in dto.Actors)
            {
                result.Add(new MovieActor()
                {
                    ActorId  = actor.ActorId,
                    Character = actor.Character
                });
            }
            return result;
        }

        private List<MovieGenre> MapMovieGenres(UpdateMovieDto dto, Movie movie)
        {
            var result = new List<MovieGenre>();
            if (dto.GenreIds is null) { return result; }

            foreach (var id in dto.GenreIds)
            {
                result.Add(new MovieGenre() { GenreId = id });
            }
            return result;
        }

    }
}

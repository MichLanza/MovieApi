using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Db;
using MovieApi.Dtos;
using MovieApi.Dtos.Movies;
using MovieApi.Dtos.NewFolder;
using MovieApi.Entities;
using MovieApi.Storage;

namespace MovieApi.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _storage;
        private readonly string container = "Movies";

        public MoviesController(AppDbContext context,
            IMapper mapper,
            IFileStorage storage)
        {
            _context = context;
            _mapper = mapper;
            _storage = storage;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] PaginationQuery pagination)
        {

            var queryable = _context.Movies.AsQueryable();
            var list = await queryable.ToListAsync();
            var data = _mapper.Map<List<MovieDto>>(list).OrderBy(x => x.Title);
            if (pagination.PageSize > 0)
            {
                var pagedResponse = PagedList<MovieDto>.Create(data, pagination.Page, pagination.PageSize);
                return Ok(pagedResponse);
            }

            return Ok(data);
        }


        [HttpGet("index")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var top = 5;
            var today = DateTime.Today;
            var nextPremier = await _context.Movies
                .Where(x => x.PremiereDate > today)
                .OrderBy(x => x.PremiereDate)
                .Take(top)
                .ToListAsync();

            var onCinemas = await _context.Movies
                .Where(x => x.OnCinema)
                .Take(top)
                .ToListAsync();

            var response = new ListMoviesDto()
            {
                NextPremiers = _mapper.Map<List<MovieDto>>(nextPremier),
                OnCinemas = _mapper.Map<List<MovieDto>>(onCinemas),
            };

            return Ok(response);
        }


        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery]MovieFiltersDto filtersDto)
        {
            var queryable = _context.Movies.AsQueryable();

            if(!string.IsNullOrEmpty(filtersDto.Title))
            {
                queryable =  queryable.Where(x=> x.Title.Contains(filtersDto.Title));   
            }

            return Ok();
        }


        [HttpGet("{id:guid}", Name = "GetMovieById")]
        public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
        {

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound(new { message = $"movie with id: {id} not found" });
            }

            var response = _mapper.Map<MovieDto>(movie);

            return response;
        }


        [HttpPost]
        public async Task<IActionResult> PostMovie([FromForm] CreateMovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);

            if (movieDto.Poster != null)
            {
                using (var ms = new MemoryStream())
                {
                    await movieDto.Poster.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var extention = Path.GetExtension(movieDto.Poster.FileName);
                    var contentType = movieDto.Poster.ContentType;
                    movie.Poster = await _storage.Save(content, extention, container, contentType);
                }
            }
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<MovieDto>(movie);
            return new CreatedAtRouteResult("GetMovieById", new { id = response.Id }, movieDto);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutMovie(Guid id, [FromForm] UpdateMovieDto dto)
        {
            var movie = await _context.Movies
                .Include(x => x.MovieGenre)
                .Include(x => x.MovieActors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound(new { message = "Pelicula no encontrada" });
            }


            movie = _mapper.Map(dto, movie);

            if (dto.Poster != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.Poster.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var extention = Path.GetExtension(dto.Poster.FileName);
                    var contentType = dto.Poster.ContentType;
                    movie.Poster = await _storage.Edit(content, extention, container, contentType, movie.Poster);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromForm] JsonPatchDocument<PatchMovieDto> dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound(new { message = "Pelicula no encontrada" });
            }


            var movieDto = _mapper.Map<PatchMovieDto>(movie);

            dto.ApplyTo(movieDto, ModelState);
            var isValid = TryValidateModel(dto);
            if (isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(dto, movie);

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var exists = await _context.Movies.AnyAsync(m => m.Id == id);

            if (!exists)
            {
                return NotFound(new { message = "Pelicula no encontrada" });
            }

            _context.Movies.Remove(new Movie() { Id = id });

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

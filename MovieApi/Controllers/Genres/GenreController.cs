using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Db;
using MovieApi.Dtos.Genre;
using MovieApi.Entities;

namespace MovieApi.Controllers.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genres = await _context.Genres.ToListAsync();
            var response = _mapper.Map<List<GenreDto>>(genres);
            return Ok(response);
        }


        [HttpGet("{id:int}", Name = "GetGenreById")]
        public async Task<IActionResult> Get(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return NotFound(new { Message = "genero no encontrado" });
            }
            var response = _mapper.Map<GenreDto>(genre);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateGenreDto dto)
        {
            var exists = await _context.Genres.AnyAsync(g => g.Name.Trim().ToUpper() == dto.Name.Trim().ToUpper());

            if (exists)
            {
                return BadRequest(new { Message = "Ya se encuentra registrado" });
            }

            var genre = _mapper.Map<Genre>(dto);

            _context.Add(genre);

            await _context.SaveChangesAsync();

            var response = _mapper.Map<GenreDto>(genre);

            return new CreatedAtRouteResult("GetGenreById", new { id = response.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] UpdateGenreDto dto, int id)
        {
            var exists = await _context.Genres.AnyAsync(g => g.Id == id);

            if (!exists)
            {
                return NotFound(new { Message = "Genero no encontrado" });
            }   
            var genre = _mapper.Map<Genre>(dto);
            genre.Id = id;
            _context.Update(genre);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "actualizado correctamente" });
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _context.Genres.AnyAsync(g => g.Id == id);

            if (!exists)
            {
                return NotFound(new { Message = "Genero no encontrado" });
            }

            _context.Remove(new Genre { Id = id });

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Borrado correctamente" });
        }

    }
}

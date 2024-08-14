using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Db;
using MovieApi.Dtos;
using MovieApi.Dtos.Actors;
using MovieApi.Entities;
using MovieApi.Extensions;
using MovieApi.Storage;

namespace MovieApi.Controllers.Actors
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string container = "Actors";

        public ActorsController(
            AppDbContext context,
            IMapper mapper,
            IFileStorage fileStorage
            )
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationQuery pagination)
        {
            var queryable = _context.Actors.AsQueryable();

            // await HttpContext.InsertPaging(queryable, pagination.PageSize);

            //var list = await queryable.Paging(pagination).ToListAsync();
            var list = await queryable.ToListAsync();
            var data = _mapper.Map<List<ActorDto>>(list).OrderBy(x => x.Name);

            if (pagination.PageSize > 0)
            {
                var pagedResponse = PagedList<ActorDto>.Create(data, pagination.Page, pagination.PageSize);
                return Ok(pagedResponse);
            }

            var response = data;
            return Ok(response);
        }

        [HttpGet("{id:guid}", Name = "GetActorById")]
        public async Task<IActionResult> Get(Guid id)
        {

            var actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor is null)
            {
                return NotFound(new { message = "Actor no encontrado" });
            }
            var response = _mapper.Map<Actor>(actor);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] UpdateActorDto dto)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor is null)
            {
                return NotFound(new { message = "Actor no encontrado" });
            }


            actor = _mapper.Map(dto, actor);

            if (dto.Photo != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.Photo.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var extention = Path.GetExtension(dto.Photo.FileName);
                    var contentType = dto.Photo.ContentType;
                    actor.Photo = await _fileStorage.Edit(content, extention, container, contentType, actor.Photo);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateActorDto dto)
        {
            var actor = _mapper.Map<Actor>(dto);

            if (dto.Photo != null)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.Photo.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var extention = Path.GetExtension(dto.Photo.FileName);
                    var contentType = dto.Photo.ContentType;
                    actor.Photo = await _fileStorage.Save(content, extention, container, contentType);
                }
            }

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<ActorDto>(actor);
            return new CreatedAtRouteResult("GetActorById", new { id = response.Id }, response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var exists = await _context.Actors.AnyAsync(actor => actor.Id == id);

            if (!exists)
            {
                return NotFound(new { message = "Actor no encontrado" });
            }

            _context.Actors.Remove(new Actor() { Id = id });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromForm] JsonPatchDocument<PatchActorDto> dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor is null)
            {
                return NotFound(new { message = "Actor no encontrado" });
            }


            var actorDto = _mapper.Map<PatchActorDto>(actor);

            dto.ApplyTo(actorDto, ModelState);
            var isValid = TryValidateModel(dto);
            if (isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(dto, actor);

            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}

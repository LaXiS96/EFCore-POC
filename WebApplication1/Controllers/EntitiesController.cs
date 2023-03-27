using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataModel;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public EntitiesController(
            IMapper mapper,
            MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //public async Task<ActionResult<ApiModel.Entity>> Post(
        //    [FromBody] ApiModel.Entity entity)
        //{
        //    // TODO
        //    await _context.Entities.Add();
        //    return NotFound();
        //}

        [HttpGet]
        public IQueryable<ApiModel.Entity> Get()
        {
            return _context.Entities
                .AsNoTracking()
                .ProjectTo<ApiModel.Entity>(_mapper.ConfigurationProvider);
        }
    }
}

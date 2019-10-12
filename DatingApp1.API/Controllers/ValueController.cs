using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using DatingApp1.API.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly DataContext _context;
        public ValueController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _context.values.ToListAsync();
            return Ok(values);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value =await _context.values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }
    }
}
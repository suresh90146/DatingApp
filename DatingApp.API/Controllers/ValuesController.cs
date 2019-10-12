using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Model.Data;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public ActionResult GetValues()
        {
            var values = _context.values.ToList();
            return Ok(values);
        }
        [HttpGet("id")]
        public ActionResult GetValue(int id)
        {
            var value = _context.values.FirstOrDefault(x => x.Id == id);
            return Ok(value);
        }

    }
}
using LolStatsAPI.Models;
using Marveldle.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Linq;

namespace Marveldle.Controllers {
    [Route("characters/comics")]
    [ApiController]
    public class ComicCharacterController : ControllerBase {

        private DataContext _context;

        public ComicCharacterController(DataContext dataContext) {
            _context = dataContext;
        }

        [HttpGet]
        public ActionResult<List<ComicCharacter>> GetAllComicCharacters() {
            return Ok(_context.ComicCharacters.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<ComicCharacter> GetComicCharacter(string id) {
            var character = _context.Find<ComicCharacter>(id);
            return character != null ? Ok(character) : NotFound(null);
        }

    }
}

using LolStatsAPI.Models;
using Marveldle.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Linq;
using Marveldle.Utils.Similarities;

namespace Marveldle.Controllers {
    [Route("characters/comics")]
    [ApiController]
    public class ComicCharacterController : ControllerBase {

        private DataContext _context;
        private IWebHostEnvironment _hostingEnvironment;

        public ComicCharacterController(DataContext dataContext, IWebHostEnvironment environment) {
            _context = dataContext;
            _hostingEnvironment = environment;
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


        [HttpGet("guess/{id}")]
        public ActionResult<ComicCharacter> GuessComicCharacter(string id) {
            Console.WriteLine(_context.CharacterPicks.Count());
            var guessedCharacter = _context.Find<ComicCharacter>(id);
            var toGuessCharacter = _context.GetLastCharacterPick().comicCharacter;
            return guessedCharacter != null ? Ok(SimilaritiesFinder.GetSimilarities(guessedCharacter, toGuessCharacter)) : NotFound(null);
        }

        [HttpPost]
        public ActionResult<int> CreateComicCharacter([FromForm] IFormFile file, [FromForm] ComicCharacter comicCharacterToAdd) {
            Console.WriteLine(_hostingEnvironment.WebRootPath);
            string comicCharacterUploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "characters", "comics", comicCharacterToAdd.Id + ".png");
            using (Stream fileStream = new FileStream(comicCharacterUploadPath, FileMode.Create)) {
                file.CopyTo(fileStream);
            }

            var character = _context.ComicCharacters.Find(comicCharacterToAdd.Id);
            if (character != null) {
                return Conflict(character);
            }
            _context.ComicCharacters.Add(comicCharacterToAdd);
            _context.SaveChanges();
            return Created(comicCharacterToAdd.Id, comicCharacterToAdd);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> DeleteComicCharacter(string id) {
            Console.WriteLine(id);
            var character = _context.ComicCharacters.Find(id);
            if (character == null) {
                return NotFound(null);
            }
            _context.Remove(character);
            _context.SaveChanges();
            return Ok();
        }


    }
}

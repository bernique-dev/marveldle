using LolStatsAPI.Models;
using Marveldle.Models;
using Marveldle.Utils.Similarities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marveldle.Controllers {
    [Route("characters/audiovisual")]
    [ApiController]
    public class AudioVisualCharacterController : ControllerBase {

        private DataContext _context;

        public AudioVisualCharacterController(DataContext dataContext) {
            _context = dataContext;
        }

        [HttpGet]
        public ActionResult<List<AudioVisualCharacter>> GetAllAudioVisualCharacters() {
            return Ok(_context.AudioVisualCharacters.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<AudioVisualCharacter> GetAudioVisualCharacter(string id) {
            var character = _context.Find<AudioVisualCharacter>(id);
            return character != null ? Ok(character) : NotFound(null);
        }

        [HttpGet("guess/{id}")]
        public ActionResult<AudioVisualCharacter> GuessAudioVisualCharacter(string id) {
            var guessedCharacter = _context.Find<AudioVisualCharacter>(id);
            var toGuessCharacter = _context.GetLastCharacterPick().audioVisualCharacter;
            return guessedCharacter != null ? Ok(SimilaritiesFinder.GetSimilarities(guessedCharacter, toGuessCharacter)) : NotFound(null);
        }


        [HttpPost]
        public ActionResult<int> CreateComicCharacter(AudioVisualCharacter audioVisualCharacterToAdd) {
            var character = _context.AudioVisualCharacters.Find(audioVisualCharacterToAdd.Id);
            if (character != null) {
                return Conflict(character);
            }
            _context.AudioVisualCharacters.Add(audioVisualCharacterToAdd);
            _context.SaveChanges();
            return Created(audioVisualCharacterToAdd.Id, audioVisualCharacterToAdd);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> DeleteComicCharacter(string id) {
            Console.WriteLine(id);
            var character = _context.AudioVisualCharacters.Find(id);
            if (character == null) {
                return NotFound(null);
            }
            _context.Remove(character);
            _context.SaveChanges();
            return Ok();
        }

    }
}

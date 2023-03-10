using LolStatsAPI.Models;
using Marveldle.Models;
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
    }
}

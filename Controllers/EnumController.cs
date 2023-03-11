using Marveldle.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marveldle.Controllers {
    [Route("enums")]
    [ApiController]
    public class EnumController : ControllerBase {

        [HttpGet("genders")]
        public ActionResult GetAllGenders() {
            return Ok(Enum.GetValues(typeof(Gender)));
        }

        [HttpGet("species")]
        public ActionResult GetAllSpecies() {
            return Ok(Enum.GetValues(typeof(Species)));
        }

        [HttpGet("charactertypes")]
        public ActionResult GetAllCharacterTypes() {
            return Ok(Enum.GetValues(typeof(CharacterType)));
        }

        [HttpGet("appearancetypes")]
        public ActionResult GetAllAppearanceTypes() {
            return Ok(Enum.GetValues(typeof(AppearanceType)));
        }

    }
}

using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Marveldle.Models {
    public class ComicCharacter : Character {

        public int ApparitionYear { get; set; }

        public ComicCharacter() : base() {}

        public ComicCharacter(string id, string name, Gender gender, CharacterType type, List<Species> species) : base(id, name, gender, type, species) {}

        public ComicCharacter(string id, string name, Gender gender, CharacterType type, List<Species> species, int apparitionYear) : base(id, name, gender, type, species) {
            ApparitionYear = apparitionYear;
        }

    }
}

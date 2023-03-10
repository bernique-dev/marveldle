using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Marveldle.Models {
    public class ComicCharacter : Character {

        public int ApparitionYear { get; set; }

        public ComicCharacter(string id, string name, Gender gender, List<Species> species) : base(id, name, gender, species) {}

        public ComicCharacter(string id, string name, Gender gender, List<Species> species, int apparitionYear) : base(id, name, gender, species) {
            ApparitionYear = apparitionYear;
        }

    }
}

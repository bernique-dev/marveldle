using System.ComponentModel.DataAnnotations;

namespace Marveldle.Models {
    public class AudioVisualCharacter : Character {

        public List<AppearanceType> AppearanceTypes { get; set; }

        public AudioVisualCharacter() : base() { }

        public AudioVisualCharacter(string id, string name, Gender gender, CharacterType type, List<Species> species) : base(id, name, gender, type, species) { }

        public AudioVisualCharacter(string id, string name, Gender gender, CharacterType type, List<Species> species, List<AppearanceType> appearanceTypes) : base(id, name, gender, type, species) {
            AppearanceTypes = appearanceTypes;
        }
    }
}

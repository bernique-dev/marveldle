using System.ComponentModel.DataAnnotations;

namespace Marveldle.Models {
    public class AudioVisualCharacter : Character {

        public List<AppearanceType> AppearanceTypes { get; set; }

        public AudioVisualCharacter(string id, string name, Gender gender, List<Species> species) : base(id, name, gender, species) { }

        public AudioVisualCharacter(string id, string name, Gender gender, List<Species> species, List<AppearanceType> appearanceTypes) : base(id, name, gender, species) {
            AppearanceTypes = appearanceTypes;
        }
    }
}

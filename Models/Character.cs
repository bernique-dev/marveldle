using System.ComponentModel.DataAnnotations;

namespace Marveldle.Models {
    public class Character {

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public List<Species> Species { get; set; }

        public Character(string id, string name, Gender gender, List<Species> species) {
            Id = id;
            Name = name;
            Gender = gender;
            Species = species;
        }
    }
}

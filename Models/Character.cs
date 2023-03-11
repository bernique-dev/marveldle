using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Marveldle.Models {
    public class Character {

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CharacterType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public List<Species> Species { get; set; }

        public Character() { }

        public Character(string id, string name, Gender gender, CharacterType type, List<Species> species) {
            Id = id;
            Name = name;
            Gender = gender;
            this.Type = type;
            Species = species;
        }
    }
}

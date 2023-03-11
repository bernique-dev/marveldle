using System.ComponentModel.DataAnnotations;

namespace Marveldle.Models {
    public class CharacterPick {

        [Key]
        public string dateId { get; set; }

        public DateTime date { get; set; }

        public AudioVisualCharacter audioVisualCharacter { get; set; }
        public ComicCharacter comicCharacter { get; set; }

        public CharacterPick() { }

        public CharacterPick(DateTime date, AudioVisualCharacter audioVisualCharacter, ComicCharacter comicCharacter) {
            this.dateId = date.Date.ToString();
            this.date = date;
            this.audioVisualCharacter = audioVisualCharacter;
            this.comicCharacter = comicCharacter;
        }

    }
}

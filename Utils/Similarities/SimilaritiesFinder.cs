using Marveldle.Models;

namespace Marveldle.Utils.Similarities
{
    public class SimilaritiesFinder {

        public static Dictionary<string, SimilarityLevel> GetSimilarities(Character guessedCharacter, Character toGuessCharacter) {
            var similarities = new Dictionary<string, SimilarityLevel>();

            similarities.Add("all", guessedCharacter.Id == toGuessCharacter.Id ? SimilarityLevel.Exact : SimilarityLevel.None);

            similarities.Add("gender", guessedCharacter.Gender == toGuessCharacter.Gender ? SimilarityLevel.Exact : SimilarityLevel.None);
            similarities.Add("type", guessedCharacter.Type == toGuessCharacter.Type ? SimilarityLevel.Exact : SimilarityLevel.None);

            var speciesSimilarity = SimilarityLevel.None;
            if (guessedCharacter.Species.Any(sp => toGuessCharacter.Species.Contains(sp))) {
                if (guessedCharacter.Species.All(sp => toGuessCharacter.Species.Contains(sp) && guessedCharacter.Species.Count == toGuessCharacter.Species.Count)) {
                    speciesSimilarity = SimilarityLevel.Exact;
                } else {
                    speciesSimilarity = SimilarityLevel.Partial;
                }
            }
            similarities.Add("species", speciesSimilarity);

            return similarities;
        }
        public static Dictionary<string, SimilarityLevel> GetSimilarities(ComicCharacter guessedCharacter, ComicCharacter toGuessCharacter) {
            var similarities = GetSimilarities(guessedCharacter as Character, toGuessCharacter as Character);

            var apparitionYearSimilarity = SimilarityLevel.Exact;
            if (guessedCharacter.ApparitionYear != toGuessCharacter.ApparitionYear) {
                apparitionYearSimilarity = guessedCharacter.ApparitionYear > toGuessCharacter.ApparitionYear ? SimilarityLevel.Lower :SimilarityLevel.Upper;
            }
            similarities.Add("apparitionYear", apparitionYearSimilarity);

            return similarities;
        }
        public static Dictionary<string, SimilarityLevel> GetSimilarities(AudioVisualCharacter guessedCharacter, AudioVisualCharacter toGuessCharacter) {
            var similarities = GetSimilarities(guessedCharacter as Character, toGuessCharacter as Character);

            var appearanceTypesSimilarities = SimilarityLevel.None;
            if (guessedCharacter.AppearanceTypes.Any(at => toGuessCharacter.AppearanceTypes.Contains(at))) {
                if (guessedCharacter.AppearanceTypes.All(at => toGuessCharacter.AppearanceTypes.Contains(at) && guessedCharacter.AppearanceTypes.Count == toGuessCharacter.AppearanceTypes.Count)) {
                    appearanceTypesSimilarities = SimilarityLevel.Exact;
                }
                else {
                    appearanceTypesSimilarities = SimilarityLevel.Partial;
                }
            }
            similarities.Add("appearanceTypes", appearanceTypesSimilarities);

            return similarities;
        }

    }
}

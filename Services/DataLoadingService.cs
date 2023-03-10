using LolStatsAPI.Models;
using Marveldle.Models;
using System;
using System.Collections;

namespace Marveldle.Services {
    public class DataLoadingService : IHostedService {
        private readonly DataContext _context;

        public DataLoadingService(IServiceProvider serviceProvider) {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            if (_context.AudioVisualCharacters.Count() == 0) {
                LoadAudioVisualCharacters();
            }
            if (_context.ComicCharacters.Count() == 0) {
                LoadComicCharacters();
            }


            return Task.CompletedTask;
        }

        private void LoadAudioVisualCharacters() {
            Console.WriteLine("LoadAudioVisualCharacters");
            List<AudioVisualCharacter> audioVisualCharacters = new List<AudioVisualCharacter>() {
                new AudioVisualCharacter("loki", "Loki", Gender.Genderfluid, CharacterType.AntiHero,
                                        new List<Species>() { Species.FrostGiant },
                                        new List<AppearanceType> { AppearanceType.Series, AppearanceType.Series, AppearanceType.Movie }),
                new AudioVisualCharacter("daredevil", "Daredevil", Gender.Male, CharacterType.Vigilante,
                                        new List<Species>() { Species.Human, Species.Mutate },
                                        new List<AppearanceType> { AppearanceType.Series, AppearanceType.Series, AppearanceType.Movie }),
                  new AudioVisualCharacter("jessicajones", "Jessica Jones", Gender.Female, CharacterType.AntiHero,
                                        new List<Species>() { Species.Human, Species.Mutate },
                                        new List<AppearanceType> { AppearanceType.Series }),
                  new AudioVisualCharacter("thanos", "Thanos", Gender.Male, CharacterType.Villain,
                                        new List<Species>() { Species.Titan },
                                        new List<AppearanceType> { AppearanceType.Movie, AppearanceType.Cartoon }),
                  new AudioVisualCharacter("thor", "Thor", Gender.Male, CharacterType.Superhero,
                                        new List<Species>() { Species.Asgardian },
                                        new List<AppearanceType> { AppearanceType.Movie, AppearanceType.Cartoon }),
                  new AudioVisualCharacter("ironman", "Iron Man", Gender.Male, CharacterType.Superhero,
                                        new List<Species>() { Species.Human },
                                        new List<AppearanceType> { AppearanceType.Movie, AppearanceType.Cartoon }),
                  new AudioVisualCharacter("modok", "M.O.D.O.K.", Gender.Male, CharacterType.Villain,
                                        new List<Species>() { Species.Human, Species.Cyborg },
                                        new List<AppearanceType> { AppearanceType.Movie, AppearanceType.Series, AppearanceType.Cartoon }),
                  new AudioVisualCharacter("spiderman", "Spider-Man", Gender.Male, CharacterType.Superhero,
                                        new List<Species>() { Species.Human, Species.Mutate },
                                        new List<AppearanceType> { AppearanceType.Movie, AppearanceType.Cartoon })
                /*
                  new AudioVisualCharacter("", "", Gender., CharacterType.,
                                        new List<Species>() {  }, "",
                                        new List<AppearanceType> {  })
                */
            };
            _context.AudioVisualCharacters.AddRange(audioVisualCharacters);
            _context.SaveChanges();
        }

        private void LoadComicCharacters() {
            Console.WriteLine("LoadComicCharacters");
            List<ComicCharacter> comicCharacters = new List<ComicCharacter>() {
                new ComicCharacter("spiderman", "Spider-Man", Gender.Male, CharacterType.Superhero,
                                        new List<Species>() { Species.Human, Species.Mutate }, 1962),
                new ComicCharacter("blackwidow", "Black Widow", Gender.Female, CharacterType.Superhero,
                                        new List<Species>() { Species.Human, Species.Mutate }, 1964),
                 new ComicCharacter("captainamerica", "Captain America", Gender.Male, CharacterType.Superhero,
                                        new List<Species>() { Species.Human }, 1940),
                 new ComicCharacter("captainmarvel", "Captain Marvel (Carol Danvers)", Gender.Female, CharacterType.Superhero,
                                        new List<Species>() { Species.Human, Species.Kree }, 1967),
                 new ComicCharacter("deadpool", "Deadpool", Gender.Male, CharacterType.AntiHero,
                                        new List<Species>() { Species.Mutant }, 1990),
                 new ComicCharacter("hulk", "Hulk", Gender.Male, CharacterType.Superhero,
                                        new List<Species>() { Species.Human, Species.Mutate }, 2000),
                 new ComicCharacter("greengoblin", "Green Goblin", Gender.Male, CharacterType.Villain,
                                        new List<Species>() { Species.Human, Species.Mutate }, 1964),
                 new ComicCharacter("doctordoom", "Doctor Doom", Gender.Male, CharacterType.Villain,
                                        new List<Species>() { Species.Human }, 1962),
                 new ComicCharacter("kangtheconqueror", "Kang the Conqueror", Gender.Male, CharacterType.Villain,
                                        new List<Species>() { Species.Human }, 1964),
                 new ComicCharacter("modok", "M.O.D.O.K. ", Gender.Male, CharacterType.Villain,
                                        new List<Species>() { Species.Human, Species.Mutate, Species.Cyborg }, 1967),
                 new ComicCharacter("mystique", "Mystique", Gender.Female, CharacterType.Superhero,
                                        new List<Species>() { Species.Mutant }, 1978)
                /*
                 new ComicCharacter("", "", Gender., CharacterType.,
                                        new List<Species>() {  }, 2000)
                */
            };
            _context.ComicCharacters.AddRange(comicCharacters);
            _context.SaveChanges();
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}

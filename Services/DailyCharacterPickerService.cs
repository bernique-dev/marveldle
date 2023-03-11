using LolStatsAPI.Models;
using Marveldle.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Marveldle.Services {
    public class DailyCharacterPickerService : BackgroundService {

        private readonly DataContext _context;

        public DailyCharacterPickerService(IServiceProvider serviceProvider) {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {

            if (!_context.CharacterPicks.ToList().Any(cp => cp.date.Date.Equals(DateTime.Now.Date))) {
                PickRandomCharacters();
            }

            do {
                int hourSpan = 24 - DateTime.Now.Hour;
                int numberOfHours = hourSpan;

                if (hourSpan == 24) {
                    if (!_context.CharacterPicks.ToList().Any(cp => cp.date.Date.Equals(DateTime.Now.Date))) {
                        PickRandomCharacters();
                    }
                    numberOfHours = 24;
                }

                await Task.Delay(TimeSpan.FromHours(numberOfHours), stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);

        }

        private void PickRandomCharacters() {
            Console.WriteLine("Picking Comic Character");
            var pickedAudioVisualCharacter = _context.AudioVisualCharacters.OrderBy(o => Guid.NewGuid()).First();
            var pickedComicCharacter = _context.ComicCharacters.OrderBy(o => Guid.NewGuid()).First();

            var characterPick = new CharacterPick(DateTime.Now, pickedAudioVisualCharacter, pickedComicCharacter);
            _context.Add(characterPick);
            _context.SaveChanges();
        }
    }
}

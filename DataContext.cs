using Marveldle.Models;
using Marveldle.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LolStatsAPI.Models
{
    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            var appearanceTypeConverter = new EnumToStringConverter<AppearanceType>();
            var speciesConverter = new EnumToStringConverter<Species>();

            modelBuilder
                .Entity<ComicCharacter>()
                .Property(c => c.Species)
                .HasConversion(
                      v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                      v => v.Split(new[] { ',' })
                        .Select(e => Enum.Parse(typeof(Species), e))
                        .Cast<Species>()
                        .ToList()
                  );
            modelBuilder
                .Entity<AudioVisualCharacter>()
                .Property(c => c.Species)
                .HasConversion(
                      v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                      v => v.Split(new[] { ',' })
                        .Select(e => Enum.Parse(typeof(Species), e))
                        .Cast<Species>()
                        .ToList()
                  );
            modelBuilder
                .Entity<AudioVisualCharacter>()
                .Property(c => c.AppearanceTypes)
                .HasConversion(
                      v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                      v => v.Split(new[] { ',' })
                        .Select(e => Enum.Parse(typeof(AppearanceType), e))
                        .Cast<AppearanceType>()
                        .ToList()
                  );
        }

        public CharacterPick GetLastCharacterPick() {
            return CharacterPicks
                .Include(cp => cp.comicCharacter)
                .Include(cp => cp.audioVisualCharacter)
                .OrderBy(cp => cp.date)
                .Last();
        }

        public DbSet<AudioVisualCharacter> AudioVisualCharacters { get; set; }
        public DbSet<ComicCharacter> ComicCharacters { get; set; }
        public DbSet<CharacterPick> CharacterPicks { get; set; }

    }
}

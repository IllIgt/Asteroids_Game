

namespace StarWars.Infrastructure
{
    class GameInfo
    {
        public string PlayerName { get; set; }
        public int ScoresCount { get; set; }
        public int DestroyedAsteroids { get; set; }

        public override string ToString()
        {
            return $"{PlayerName} : scores {ScoresCount}; asteroids : {DestroyedAsteroids}";
        }

        public void Deconstruct(out string Name, out int Scores, out int Asteroids)
        {
            Name = PlayerName;
            Scores = ScoresCount;
            Asteroids = DestroyedAsteroids;
        }
    }
}

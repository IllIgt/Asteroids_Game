using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class GameInfoStorage : IEnumerable<GameInfo>
    {
        private ICollection<GameInfo> _Games;

        public IEnumerator<GameInfo> GetEnumerator()
        {
            foreach (var info in _Games)
                yield return info;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

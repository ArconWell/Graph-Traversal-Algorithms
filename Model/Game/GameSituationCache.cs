using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class GameSituationCache
    {
        private static List<GameSituation> cache = new List<GameSituation>();

        public static void Add(GameSituation gameSituation)
        {
            if (!cache.Contains(gameSituation))
                cache.Add(gameSituation);
        }

        public static bool Remove(GameSituation gameSituation)
        {
            return cache.Remove(gameSituation);
        }

        public static bool Contains(GameSituation gameSituation)
        {
            return cache.Contains(gameSituation);
        }
    }
}

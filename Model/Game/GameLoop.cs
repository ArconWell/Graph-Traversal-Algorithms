using Model.EvaluationFunctions;
using Model.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game
{
    public class GameLoop
    {
        private GameSituation gameSituation;
        private ISearch searchType = null;

        public ISearch SearchType
        {
            get
            {
                if (searchType is null)
                {
                    throw new NullReferenceException("SearchType property is null");
                }
                return searchType;
            }
            set
            {
                searchType = value;
            }
        }

        public GameSituation GameSituation { get => gameSituation; private set => gameSituation = value; }

        public GameLoop(params Tower[] towers)
        {
            int inGameRingsCount = 0;
            Tower startTower = null;
            bool flag = false;

            if (towers.Length < 3)
            {
                throw new Exception("Game rules require at least 3 towers");
            }

            foreach (Tower tower in towers)
            {
                if (tower.RingsCount > 0)
                {
                    if (flag is false)
                    {
                        inGameRingsCount = tower.RingsCount;
                        startTower = tower;
                        flag = true;
                    }
                    else
                    {
                        throw new Exception("Game rules require all rings to be on the same tower at the start of the game");
                    }
                }
            }
            if (flag is false)
            {
                throw new Exception("Game rules require rings to play the game");
            }

            GameSituation = new GameSituation(towers, null, inGameRingsCount, startTower);
        }

        public GameLoop(ISearch searchType, params Tower[] towers) : this(towers)
        {
            SearchType = searchType;
        }

        public (bool result, Stack<Step> steps) Start()
        {
            return SearchType.Search(GameSituation);
        }
    }
}

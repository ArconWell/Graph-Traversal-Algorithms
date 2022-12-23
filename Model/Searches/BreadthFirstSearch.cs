using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Searches
{
    public class BreadthFirstSearch : ISearch
    {
        public (bool result, Stack<Step> steps) Search(GameSituation gameSituation)
        {
            List<GameSituation> gameSituations = new List<GameSituation>
            {
                gameSituation
            };
            return Search(gameSituations);
        }

        public (bool result, Stack<Step> steps) Search(List<GameSituation> gameSituations)
        {
            (bool result, Stack<Step> steps) value = (false, new Stack<Step>());
            List<Step> possibleSteps = new List<Step>();

            if (gameSituations.Count == 0)
            {
                return value;
            }

            foreach (GameSituation gameSituation in gameSituations)
            {
                if (GameSituationCache.Contains(gameSituation))
                {
                    continue;
                }
                else
                {
                    GameSituationCache.Add(gameSituation);
                    possibleSteps.AddRange(gameSituation.GetAllPossibleSteps());
                }
            }

            List<GameSituation> nextLevelGameSituations = new List<GameSituation>();

            foreach (Step step in possibleSteps)
            {
                GameSituation newGameSituation = step.MakeStep();

                if (newGameSituation.IsGameFinishedSuccessfully())
                {
                    value.result = true;
                    value.steps.Push(new Step(newGameSituation, step.ToTower, step.ToTower));
                    value.steps.Push(step);
                    return value;
                }

                nextLevelGameSituations.Add(newGameSituation);
            }

            value = Search(nextLevelGameSituations);

            if (value.result)
            {
                value.steps.Push(value.steps.Peek().GameSituation.PreviousStep);
            }

            return value;
        }
    }
}

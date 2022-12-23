using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Model.Searches
{
    public class DepthFirstSearch : ISearch
    {
        public (bool result, Stack<Step> steps) Search(GameSituation gameSituation)
        {
            (bool result, Stack<Step> steps) value = (false, new Stack<Step>());

            if (GameSituationCache.Contains(gameSituation))
            {
                return value;
            }
            GameSituationCache.Add(gameSituation, false);

            Stack<Step> possibleSteps = new Stack<Step>(gameSituation.GetAllPossibleSteps());

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
                value = Search(newGameSituation);
                if (value.result)
                {
                    value.steps.Push(step);
                    return value;
                }
            }
            return value;
        }
    }
}

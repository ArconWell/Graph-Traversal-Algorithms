using Model.EvaluationFunctions;
using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Searches
{
    public class GradientSearch : ISearch
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
            IEvaluationFunction evaluationFunction = new EvaluationFunction();
            Stack<Step> sortedPossibleStepsByEvaluationFunction = evaluationFunction.Execute(possibleSteps);

            foreach (Step step in sortedPossibleStepsByEvaluationFunction)
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
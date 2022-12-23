using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EvaluationFunctions
{
    public class EvaluationFunction : IEvaluationFunction
    {
        public Stack<Step> Execute(Stack<Step> steps)
        {
            Stack<Step> sortedSteps = new Stack<Step>();
            Stack<Step>notSortedStep = new Stack<Step>();
            foreach (Step step in steps)
            {
                if (!step.ToTower.Equals(GameSituation.StartTower))
                {
                    sortedSteps.Push(step);
                }
                else
                {
                    notSortedStep.Push(step);
                }
            }
            foreach(Step step in notSortedStep)
            {
                sortedSteps.Push(step);
            }
            return sortedSteps;
        }
    }
}

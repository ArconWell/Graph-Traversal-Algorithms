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
            List<Step> stepsList = new List<Step>(steps);

            stepsList.Sort((step1, step2) => step1.ToTower.RingsCount.CompareTo(step2.ToTower.RingsCount));

            return new Stack<Step>(stepsList);
        }
    }
}

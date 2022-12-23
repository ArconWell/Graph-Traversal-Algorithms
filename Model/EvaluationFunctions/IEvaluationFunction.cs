using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EvaluationFunctions
{
    public interface IEvaluationFunction
    {
        Stack<Step> Execute(Stack<Step> steps);
    }
}

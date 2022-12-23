using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Searches
{
    public interface ISearch
    {
        (bool result, Stack<Step> steps) Search(GameSituation gameSituation);
    }
}

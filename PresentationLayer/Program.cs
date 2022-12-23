    using Model;
using Model.Game;
using Model.Searches;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLoop game = new GameLoop(new Tower(new Ring(3), new Ring(2), new Ring(1)),
                new Tower(),
                new Tower());
            game.SearchType = new DepthFirstSearch();
            var (result, steps) = game.Start();

            Console.WriteLine($"Решение найдено: {result}\n");

            int i = 0;
            foreach (Step step in steps)
            {
                i++;
                Console.WriteLine($"Ход {i}:\n");
                Console.WriteLine(step.GameSituation.ToString());
            }
   
            Console.ReadLine();
        }
    }
}

    using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GameSituation : ICloneable
    {
        private Tower[] towers;
        private Step previousStep;
        private static int inGameRingsCount;
        private static Tower startTower;

        /// <summary>
        /// For initialization purposes only
        /// </summary>
        public GameSituation(Tower[] towers, Step previousStep, int inGameRingsCount, Tower startTower)
            : this(towers, previousStep)
        {
            InGameRingsCount = inGameRingsCount;
            StartTower = startTower;
        }

        public GameSituation(Tower[] towers, Step previousStep)
        {
            this.towers = towers;
            this.previousStep = previousStep;
        }

        public Tower[] Towers { get => towers; }
        public Step PreviousStep { get => previousStep; set => previousStep = value; }
        public static Tower StartTower { get => startTower; private set => startTower = value; }
        public static int InGameRingsCount { get => inGameRingsCount; private set => inGameRingsCount = value; }

        public Step[] GetAllPossibleSteps()
        {
            List<Step> steps = new List<Step>();

            for (int i = 0; i < Towers.Length; i++)
            {
                for (int j = 0; j < Towers.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (Step.CanMakeStep(Towers[i], Towers[j]))
                    {
                        Step step = new Step(this, Towers[i], Towers[j]);

                        if (PreviousStep != null && PreviousStep.IsReversed(step))
                        {
                            continue;
                        }

                        steps.Add(step);
                    }
                }
            }
            return steps.ToArray();
        }

        public bool IsGameFinishedSuccessfully()
        {
            foreach (Tower tower in Towers)
            {
                if (tower.RingsCount == InGameRingsCount && !tower.Equals(StartTower))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int i = 0, j = 0;
            foreach (Tower tower in Towers)
            {
                i++;
                result.Append($"\tБашня {i}:\n");
                foreach (Ring ring in tower.Rings)
                {
                    j++;
                    result.Append($"\t\t{j}) Кольцо размером {ring.Size}\n");
                }
                j = 0;
            }
            return result.ToString();
        }

        public object Clone()
        {
            Tower[] towers = new Tower[Towers.Length];
            for (int i = 0; i < Towers.Length; i++)
            {
                towers[i] = (Tower)Towers[i].Clone();
            }
            return new GameSituation(towers, PreviousStep);
        }

        public override bool Equals(object obj)
        {
            if (obj is GameSituation gameSituation)
            {
                if (Towers.Length != gameSituation.Towers.Length)
                {
                    return false;
                }

                for (int i = 0; i < Towers.Length; i++)
                {
                    if (!Towers[i].FullEquals(gameSituation.Towers[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return -819024293 + EqualityComparer<Tower[]>.Default.GetHashCode(Towers);
        }
    }
}

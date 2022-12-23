using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game
{
    public class Step
    {
        public Step(GameSituation gameSituation, Tower fromTower, Tower toTower)
        {
            GameSituation = gameSituation;
            FromTower = fromTower;
            ToTower = toTower;
        }

        public GameSituation GameSituation { get; set; }

        public Tower FromTower { get; }

        public Tower ToTower { get; }

        public static bool CanMakeStep(Tower fromTower, Tower toTower)
        {
            return !(fromTower.RingsCount == 0 ||
                (toTower.RingsCount != 0 && fromTower.TopRing.Size > toTower.TopRing.Size));
        }

        public static GameSituation MakeStep(GameSituation gameSituation, Tower fromTower, Tower toTower)
        {
            if (CanMakeStep(fromTower, toTower))
            {
                int fromTowerIndexInGameSituation = Array.IndexOf(gameSituation.Towers, fromTower);
                int toTowerIndexInGameSituation = Array.IndexOf(gameSituation.Towers, toTower);

                GameSituation newGameSituation = (GameSituation)gameSituation.Clone();

                Ring fromTowerTopRing = newGameSituation.Towers[fromTowerIndexInGameSituation].DeleteTopRing();
                newGameSituation.Towers[toTowerIndexInGameSituation].AddRingAtTop(fromTowerTopRing);

                return newGameSituation;
            }
            return gameSituation;
        }

        public GameSituation MakeStep()
        {
            GameSituation newGameSituation = MakeStep(GameSituation, FromTower, ToTower);
            newGameSituation.PreviousStep = this;
            return newGameSituation;
        }

        public bool IsReversed(Step step)
        {
            return FromTower.Equals(step.ToTower) && ToTower.Equals(step.FromTower);
        }
    }
}

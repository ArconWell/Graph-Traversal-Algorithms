using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tower : ICloneable
    {
        public static int IdCounter { get; private set; } = 0;

        public int Id { get; }

        public Stack<Ring> Rings { get; }

        public Tower() : this(new Ring[0])
        {
        }

        private Tower(int id) : this(id, new Ring[0])
        {
        }

        public Tower(params Ring[] rings)
        {
            this.Id = IdCounter;
            IdCounter++;

            if (rings.Length != 0)
            {
                Rings = new Stack<Ring>(rings);
            }
            else
            {
                Rings = new Stack<Ring>();
            }
        }

        private Tower(int id, params Ring[] rings) : this(rings)
        {
            this.Id = id;
        }

        public int RingsCount { get { return Rings.Count; } }

        public Ring TopRing { get { return Rings.Peek(); } }

        public void AddRingAtTop(Ring ring)
        {
            Rings.Push(ring);
        }

        public Ring DeleteTopRing()
        {
            return Rings.Pop();
        }

        public override bool Equals(object obj)
        {
            return obj is Tower tower &&
                   Id.Equals(tower.Id);
        }

        public bool FullEquals(object obj)
        {
            Tower tower = obj as Tower;
            Ring[] rings1 = Rings.ToArray();
            Ring[] rings2 = tower.Rings.ToArray();

            if (rings1.Length != rings2.Length)
            {
                return false;
            }

            for (int i = 0; i < rings1.Length; i++)
            {
                if (rings1[i].Id != rings2[i].Id)
                {
                    return false;
                }
            }

            return Equals(obj);
        }

        public object Clone()
        {
            Ring[] ringsArray = Rings.ToArray();
            Ring[] newRings = new Ring[Rings.Count];

            for (int i = 0; i < Rings.Count; i++)
            {
                newRings[Rings.Count - i - 1] = (Ring)ringsArray[i].Clone();
            }
            return new Tower(Id, newRings);
        }

        public override int GetHashCode()
        {
            int hashCode = 2132781214;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Stack<Ring>>.Default.GetHashCode(Rings);
            hashCode = hashCode * -1521134295 + EqualityComparer<Ring>.Default.GetHashCode(TopRing);
            return hashCode;
        }
    }
}

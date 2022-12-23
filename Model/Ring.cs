using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ring : ICloneable
    {
        public static int IdCounter { get; private set; } = 0;

        public int Id { get; }

        public int Size { get; }

        public Ring(int size)
        {
            Id = IdCounter;
            IdCounter++;

            Size = size;
        }

        private Ring(int id, int size) : this(size)
        {
            Id = id;
        }

        public object Clone()
        {
            return new Ring(Id, Size);
        }
    }
}

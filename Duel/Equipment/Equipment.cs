using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tournament
{
    [Serializable]
    public class Equipment
    {
        public string Type;
        public string Name;
        public int Hands;
        public Switch Chance;

        public virtual Equipment Instantiate() {
            return this;
        }
    }
}

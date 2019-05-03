using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class DamageModifier
    {
        public int Add;
        public int Multipl;
        public Counter Count;
        public DamageModifier(int add, int multipl, intI count = null)
        {
            if (count == null) count = intI.Infinity;
            Add = add;
            Multipl = multipl;
            Count = new Counter(count);
        }

        public void Apply(ref int val)
        {
            if (!Count.IsTopped())
            {
                val = val * Multipl + Add;
                Count.Add();
            }
        }
    }
}

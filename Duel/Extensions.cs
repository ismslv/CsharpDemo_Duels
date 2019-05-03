using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    [Serializable]
    public class Switch
    {
        public bool[] takes;
        public int pointer;
        private readonly string pattern;
        public Switch()
        {
            takes = new bool[0];
            pointer = 0;
        }
        public Switch(string pattern)
        {
            var p = pattern.Split("/"[0]);
            takes = new bool[int.Parse(p[1])];
            for (int i = 0; i < takes.Length; i++)
            {
                takes[i] = i < int.Parse(p[0]);
            }
            pointer = 0;
            this.pattern = pattern;
        }
        public bool Next()
        {
            pointer++;
            if (pointer == takes.Length) pointer = 0;
            return Current();
        }
        public bool Current()
        {
            return takes[pointer];
        }

        public string ToPattern()
        {
            return pattern;
        }
    }

    [Serializable]
    public class intI
    {
        public int Val;
        public bool IsInfinity;

        public intI(int val, bool isInfinite = false)
        {
            Val = val;
            IsInfinity = isInfinite;
        }

        public intI(string data)
        {
            if (data == "∞")
            {
                Val = 0;
                IsInfinity = true;
            }
            else
            {
                IsInfinity = false;
                int.TryParse(data, out Val);
            }
        }

        public override string ToString()
        {
            if (IsInfinity) return "Infinity";
            return Val.ToString();
        }

        public string ToPattern()
        {
            return IsInfinity ? "∞" : Val.ToString();
        }

        public static intI Infinity {
            get
            {
                return new intI(0, true);
            }
        }
    }

    [Serializable]
    public class Counter
    {
        public intI Max;
        public int Current;

        public Counter(string max)
        {
            Max = new intI(max);
            Current = 0;
        }

        public Counter(int max)
        {
            Max = new intI(max);
            Current = 0;
        }
        public Counter(intI max)
        {
            Max = max;
            Current = 0;
        }

        public void Set(int val = 0)
        {
            Current = val;
        }

        public bool Add(int val = 1)
        {
            Current += val;
            return IsTopped();
        }

        public bool IsTopped()
        {
            if (Max.IsInfinity)
            {
                return false;
            } else
            {
                return Current >= Max.Val;
            }
        }
    }
}

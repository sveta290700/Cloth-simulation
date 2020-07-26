using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    public class Checks
    {
        public static bool IsEmpty(string word)
        {
            if (word == "")
                return true;
            else return false;
        }
        public static bool IsNumber(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (!(word[i] >= '0' && word[i] <= '9'))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsValidNumber(int num, int n, int m)
        {
            if ((num >= n) && (num <= m))
                return true;
            else return false;
        }
    }
}

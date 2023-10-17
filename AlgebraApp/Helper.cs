using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraApp
{
    static class Helper
    {
        public static int Faktorialis(int x)
        {
            int faktoriális = 1;
            for(int i=0;i<=x;i++)
            {
                faktoriális *= i;
            }
            return faktoriális;
        }
        public static int Kombinacio(int n, int k)
        {
            return Faktorialis(n) / (Faktorialis(k) * Faktorialis(n - k));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraApp
{
    internal class DiederElem
    {
        public static uint CSOPORT_RENDJE;
        public static uint N;
        static DiederElem()
        {
            Console.Write("Adja meg a diédercsoport rendjét: ");
            CSOPORT_RENDJE = uint.Parse(Console.ReadLine());
            N = (uint)Math.Log2(CSOPORT_RENDJE);
        }

        public DiederElem(ulong aPow, byte bPow)
        {
            this.aPow = aPow;
            this.bPow = bPow;
        }

        public ulong aPow;
        public byte bPow;

        public DiederElem Inverz
        {
            get
            {
                if (bPow == 1)
                {
                    return new DiederElem(aPow, bPow);
                }
                return new DiederElem(((ulong)Math.Pow(2, N - 1) - aPow) % (CSOPORT_RENDJE / 2), 0);
            }
        }

        /*Inverz megtalálása általános elmélet alapján:
        public DiederElem Inverz
        {
            get
            {
                DiederElem szorzat=new DiederElem(aPow,bPow);
                DiederElem osszeg=new DiederElem(aPow,bPow);
                while(true)
                {
                    szorzat*=szorzat;
                    if(szorzat==1)
                    {
                        return osszeg;
                    }
                    osszeg=new DiederElem((osszeg.Apow+szorzat.Apow)%(CSOPORT_RENDJE/2),(osszeg.Bpow+szorzat.Bpow)%2);
                }
            }
        }
        */

        public static DiederElem operator *(DiederElem x, DiederElem y)
        {
            ulong aPow = x.aPow;
            if (x.bPow == 1)
            {
                aPow += (uint)(Math.Pow(2, N - 1)) - 1 * y.aPow;
            }
            else aPow += y.aPow;
            byte bPow;
            if ((x.bPow == 0 && y.bPow == 1) || (x.bPow == 1 && y.bPow == 0))
                bPow = 1;
            else bPow = 0;
            aPow %= (uint)Math.Pow(2, N - 1);
            return new DiederElem(aPow, bPow);
        }

        //public static int Feloldas(DiederElem x, DiederElem y, DiederElem z)
        //{
        //    DiederElem[] temp=new DiederElem[3];
        //    temp[0] = x;
        //    temp[1] = y;
        //    temp[2] = z;
        //    int eredmeny = 0;
        //    return Feloldas(temp, eredmeny);
        //}

        //public static int Feloldas(DiederElem[] elemek, int hossz)
        //{
        //    DiederElem x = elemek[0].Inverz * elemek[1].Inverz * elemek[0] * elemek[1];
        //    DiederElem y = elemek[0].Inverz * elemek[2].Inverz * elemek[0] * elemek[2];
        //    DiederElem z = elemek[1].Inverz * elemek[2].Inverz * elemek[1] * elemek[2];
        //    hossz++;
        //    if (x.ToString() == "1" && y.ToString() == "1" && z.ToString() == "1")
        //    {
        //        return hossz;
        //    }
        //    elemek[0] = x;
        //    elemek[1] = y;
        //    elemek[2] = z;
        //    return Feloldas(elemek, hossz);
        //}

        public override string ToString()
        {
            if (aPow == 0 && bPow == 0) return "1";
            if (aPow == 0 && bPow == 1) return "b";
            if (aPow == 1 && bPow == 0) return "a";
            if (aPow == 1 && bPow == 1) return "ab";
            return $"a^{(aPow == 1 ? string.Empty : aPow)}{(bPow == 0 ? string.Empty : "b")}";
        }

        public override bool Equals(object obj)
        {
            DiederElem masik = (DiederElem)obj;

            return this.aPow == masik.aPow && this.bPow == masik.bPow;
        }
    }
}

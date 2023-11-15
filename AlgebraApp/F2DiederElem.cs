using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraApp
{
    internal class F2DiederElem
    {
        public List<DiederElem> reszhalmaz;

        public F2DiederElem(List<DiederElem> reszhalmaz)
        {
            this.reszhalmaz = reszhalmaz;
        }

        public F2DiederElem(params DiederElem[] elemek) 
        {
            reszhalmaz = new List<DiederElem>();
            foreach(DiederElem elem in elemek)
            {
                

                reszhalmaz.Add(elem);
            }
        }

        public static F2DiederElem operator *(F2DiederElem x, F2DiederElem y)
        {
            List<DiederElem> temp = new List<DiederElem>();
            foreach(DiederElem elem1 in x.reszhalmaz)
            {
                foreach(DiederElem elem2 in y.reszhalmaz)
                {
                    DiederElem szorzat=elem1*elem2;
                    if (temp.Contains(szorzat))
                    {
                        temp.Remove(szorzat);
                    }
                    else
                    {
                        temp.Add(szorzat);
                    }
                }
            }
            return new F2DiederElem(temp);
        }

        //public static F2DiederElem operator+(F2DiederElem x,F2DiederElem y)
        //{
        //    F2DiederElem temp = new F2DiederElem();
        //    foreach(DiederElem elem in x.reszhalmaz)
        //    {
        //        temp.reszhalmaz.Add(elem);
        //    }
        //    foreach(DiederElem elem in y.reszhalmaz)
        //    {
        //        if (temp.reszhalmaz.Contains(elem))
        //        {
        //            temp.reszhalmaz.Remove(elem);
        //        }
        //        else
        //        {
        //            temp.reszhalmaz.Add(elem);
        //        }
        //    }
        //    return temp;
        //}
        

        public F2DiederElem Inverz
        {
            get
            {
                F2DiederElem osszeg = new F2DiederElem(new DiederElem(0, 0));
                F2DiederElem szorzat = new F2DiederElem(reszhalmaz);
                while (true)
                {
                    osszeg = osszeg * szorzat;
                    szorzat *= szorzat;
                    if (szorzat.reszhalmaz.Count == 1 && szorzat.reszhalmaz[0].ToString() == "1")
                    {
                        return osszeg;
                    }
                    else if (szorzat.reszhalmaz.Count == 0) return new F2DiederElem();
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (reszhalmaz.Count == 0)
            {
                return "0";
            }
            sb.Append(reszhalmaz[0]);
            for(int i = 1; i < reszhalmaz.Count; i++)
            {
                sb.AppendFormat($" + {reszhalmaz[i]}");
            }
            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            if(obj!=null&&obj is F2DiederElem)
            {
                F2DiederElem other = (F2DiederElem)obj;
                if (other.reszhalmaz.Count == this.reszhalmaz.Count)
                {
                    foreach(DiederElem otherElem in other.reszhalmaz)
                    {
                        if (!reszhalmaz.Contains(otherElem))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}

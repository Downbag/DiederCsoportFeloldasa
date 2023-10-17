using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraApp
{
    internal class F2D2nCsoportElem
    {
        List<int> poziciok=new List<int>();

        public F2D2nCsoportElem()
        {
            poziciok.Add(0);
        }

        public F2DiederElem JelenlegiElem()
        {
            List<DiederElem> diederek = new List<DiederElem>();
            DiederCsoport csoport = new DiederCsoport(DiederElem.CSOPORT_RENDJE);
            foreach (int i in poziciok)
            {
                diederek.Add(csoport[i]);
            }
            return new F2DiederElem(diederek);
        }

        bool Lepes()
        {
            for(int i =poziciok.Count-1; i >-1; i--)
            {
                if (poziciok[i] < DiederElem.CSOPORT_RENDJE-1)
                {
                    int j = 1;
                    bool problema_fent_all = true;
                    while (problema_fent_all)
                    {
                        if (poziciok.Contains(poziciok[i] + j)) j++;
                        else problema_fent_all = false;
                    }
                    poziciok[i] += j;
                    if (poziciok[i] >= DiederElem.CSOPORT_RENDJE)
                    {
                        i++; continue;
                    }
                    return true;
                }
                else if (i == 0 && poziciok.Count<DiederElem.CSOPORT_RENDJE)
                {
                    poziciok.Add(0);
                    poziciok.Add(0);
                    for(int j = 0; j < poziciok.Count; j++)
                    {
                        poziciok[j] = j;
                    }
                    if(poziciok.Count>DiederElem.CSOPORT_RENDJE)
                    return true;
                }
                else
                {
                    poziciok[i] = -1;
                    int j = 0;
                    bool problema_fent_all=true;
                    while (problema_fent_all)
                    {
                        if(poziciok.Contains(j)) j++;
                        else problema_fent_all=false;
                    }
                    poziciok[i] = j;
                }
            }
            return false;
        }

        public bool KovetkezoElem()
        {
            bool a = Lepes();
            return a;
        }
    }
}

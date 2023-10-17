using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AlgebraApp
{
    class DiederCsoport
    {
        public DiederCsoport(uint elemekDb)
        {
            elemek = new List<DiederElem>();
            for (uint i = 0; i < elemekDb / 2; i++)
            {
                elemek.Add(new DiederElem(i, 0));
                elemek.Add(new DiederElem(i, 1));
            }
        }

        public List<DiederElem> elemek;

        private List<DiederElem> KommutatorReszcsoport(List<DiederElem> csoport)
        {
            List<DiederElem> temp = new List<DiederElem>();
            for (int i = 0; i < csoport.Count; i++)
            {
                for (int j = 0; j < csoport.Count; j++)
                {
                    DiederElem kommutator = csoport[i].Inverz * csoport[j].Inverz * csoport[i] * csoport[j];
                    if (!temp.Contains(kommutator))
                        temp.Add(kommutator);
                }
            }
            foreach(DiederElem d in temp)
            {
                foreach(DiederElem D in temp)
                {
                    DiederElem szorzat = d * D;
                    if (!temp.Contains(szorzat))
                    {
                        temp.Add(szorzat);
                    }
                }
            }
            return temp;
        }

        private bool Feloldhato(List<DiederElem> csoport)
        {
            List<DiederElem> kommutatorReszcsoport = KommutatorReszcsoport(csoport);
            if (kommutatorReszcsoport.Count == 1)
            {
                DiederElem elem = kommutatorReszcsoport.First();
                if (elem.aPow == 0 && elem.bPow == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Feloldhato(out int hossz)
        {           
            hossz = 1;
            return Feloldhato(elemek, ref hossz);
        }
        public bool Feloldhato(List<DiederElem> csoport, ref int hossz)
        {
            List<DiederElem> kommutatorReszcsoport = KommutatorReszcsoport(csoport);
            if (Feloldhato(kommutatorReszcsoport))
            {
                hossz++;
                DiederElem elem = kommutatorReszcsoport.First();
                if (elem.aPow == 0 && elem.bPow == 0)
                {
                    return true;
                }
            }
            return Feloldhato(kommutatorReszcsoport, ref hossz);
        }
        public DiederElem this[int a]
        {
            get
            {
                return elemek[a];
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraApp
{

    class F2D8Csoport: IEnumerable<F2D8Csoport>
    {
        public List<F2DiederElem> f2diederek = new List<F2DiederElem>();
        long position = -1;
        public F2DiederElem Current
        {
            get
            {
                try
                {
                    return f2diederek[(int)position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public F2D8Csoport()
        {
            DiederCsoport diederCsoport = new DiederCsoport(8);

            f2diederek.Add(new F2DiederElem());

            foreach (DiederElem elem in diederCsoport.elemek)
            {
                f2diederek.Add(new F2DiederElem(elem));
            }

            for (int j = 0; j < 7; j++)
            {
                for (int k = j + 1; k < 8; k++)
                {
                    f2diederek.Add(new F2DiederElem(diederCsoport.elemek[k], diederCsoport.elemek[j]));
                }
            }

            for (int i1 = 0; i1 < 6; i1++)
            {
                for (int i2 = i1 + 1; i2 < 7; i2++)
                {
                    for (int i3 = i2 + 1; i3 < 8; i3++)
                    {
                        f2diederek.Add(new F2DiederElem(diederCsoport.elemek[i1], diederCsoport.elemek[i2], diederCsoport.elemek[i3]));
                    }
                }
            }

            for (int i1 = 0; i1 < 5; i1++)
            {
                for (int i2 = i1 + 1; i2 < 6; i2++)
                {
                    for (int i3 = i2 + 1; i3 < 7; i3++)
                    {
                        for (int i4 = i3 + 1; i4 < 8; i4++)
                        {
                            f2diederek.Add(new F2DiederElem(diederCsoport.elemek[i1], diederCsoport.elemek[i2], diederCsoport.elemek[i3], diederCsoport.elemek[i4]));
                        }
                    }
                }
            }
            for (int i1 = 0; i1 < 4; i1++)
            {
                for (int i2 = i1 + 1; i2 < 5; i2++)
                {
                    for (int i3 = i2 + 1; i3 < 6; i3++)
                    {
                        for (int i4 = i3 + 1; i4 < 7; i4++)
                        {
                            for (int i5 = i4 + 1; i5 < 8; i5++)
                            {
                                f2diederek.Add(new F2DiederElem(diederCsoport.elemek[i1], diederCsoport.elemek[i2],
                                    diederCsoport.elemek[i3], diederCsoport.elemek[i4], diederCsoport.elemek[i5]));
                            }
                        }
                    }
                }
            }
            for (int i1 = 0; i1 < 3; i1++)
            {
                for (int i2 = i1 + 1; i2 < 4; i2++)
                {
                    for (int i3 = i2 + 1; i3 < 5; i3++)
                    {
                        for (int i4 = i3 + 1; i4 < 6; i4++)
                        {
                            for (int i5 = i4 + 1; i5 < 7; i5++)
                            {
                                for (int i6 = i5 + 1; i6 < 8; i6++)
                                {
                                    f2diederek.Add(new F2DiederElem(diederCsoport.elemek[i1], diederCsoport.elemek[i2],
                                        diederCsoport.elemek[i3], diederCsoport.elemek[i4], diederCsoport.elemek[i5], diederCsoport.elemek[i6]));
                                }
                            }
                        }
                    }
                }
            }
            for (int i1 = 0; i1 < 2; i1++)
            {
                for (int i2 = i1 + 1; i2 < 3; i2++)
                {
                    for (int i3 = i2 + 1; i3 < 4; i3++)
                    {
                        for (int i4 = i3 + 1; i4 < 5; i4++)
                        {
                            for (int i5 = i4 + 1; i5 < 6; i5++)
                            {
                                for (int i6 = i5 + 1; i6 < 7; i6++)
                                {
                                    for (int i7 = i6 + 1; i7 < 8; i7++)
                                    {
                                        f2diederek.Add(new F2DiederElem(diederCsoport.elemek[i1], diederCsoport.elemek[i2],
                                            diederCsoport.elemek[i3], diederCsoport.elemek[i4], diederCsoport.elemek[i5], diederCsoport.elemek[i6],
                                            diederCsoport.elemek[i7]));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            f2diederek.Add(new F2DiederElem(diederCsoport.elemek));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (F2DiederElem elem in f2diederek)
            {
                sb.Append("\nElem: ");
                sb.Append(elem.ToString());
            }
            sb.AppendFormat($"\nElemek száma: {f2diederek.Count}");
            return sb.ToString();
        }

        public string Szorzatok()
        {
            StringBuilder sb = new StringBuilder();
            long counter = 0;
            foreach (F2DiederElem x in f2diederek)
            {
                foreach (F2DiederElem y in f2diederek)
                {
                    sb.AppendFormat($"{x}*{y}={x * y}\n");
                    counter++;
                }
            }
            sb.AppendFormat($"Szorzások száma: {counter}");
            return sb.ToString();
        }

        public int Feloldas()
        {
            int ered = 0;
            return Feloldas(f2diederek, ered);
        }

        //public F2DiederElem Inverz(F2DiederElem x)
        //{
        //    foreach(F2DiederElem y in f2diederek)
        //    {
        //        F2DiederElem inverz = x * y;
        //        if(inverz.reszhalmaz.Count==1 && inverz.reszhalmaz[0].ToString()=="1")
        //        {
        //            return y;
        //        }
        //    }
        //    return new F2DiederElem();
        //}

        public int Feloldas(List<F2DiederElem> diederek, int ered)
        {
            List<F2DiederElem> temp = new List<F2DiederElem>();
            foreach (F2DiederElem x in diederek)
            {
                foreach (F2DiederElem y in diederek)
                {
                    F2DiederElem kommutator = x.Inverz * y.Inverz * x * y;
                    if (!temp.Contains(kommutator))
                    {
                        temp.Add(kommutator);
                    }
                }
            }
            bool bovult_e = true;
            while (bovult_e)
            {
                bovult_e = false;
                List<F2DiederElem> temp2 = new List<F2DiederElem>(temp);
                for (int i = 0; i < temp2.Count; i++)
                {
                    for (int j = 0; j < temp2.Count; j++)
                    {
                        F2DiederElem szorzat = temp2[i] * temp2[j];
                        if (szorzat != null && szorzat != new F2DiederElem() && !temp.Contains(szorzat))
                        {
                            temp.Add(szorzat);
                            bovult_e = true;
                        }
                    }
                }
                temp = temp2;
            }
            if (temp.Count == 0 ||
                (temp.Count == 1 && (temp[0] == new F2DiederElem() || temp[0] == new F2DiederElem(new DiederElem(0, 0)))) ||
                (temp.Count == 2 && temp.Contains(new F2DiederElem()) && temp.Contains(new F2DiederElem(new DiederElem(0, 0)))))
            {
                return ered + 1;
            }
            return Feloldas(temp, ered + 1);
        }

        public bool MoveNext()
        {
            position++;
            return position < f2diederek.Count;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<F2D8Csoport> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

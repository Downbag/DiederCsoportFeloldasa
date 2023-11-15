using System.Reflection;

namespace AlgebraApp
{
    internal class Program
    {
        static F2DiederElem Kommutator(F2DiederElem x, F2DiederElem y)
        {
            return x.Inverz * y.Inverz * x * y;
        }

        static int ReszFeloldas(List<F2DiederElem> list, int ered)
        {
            List<F2DiederElem> f2diederek= new List<F2DiederElem>();
            for(int i = 0; i < list.Count/2; i+=2)
            {
                f2diederek.Add(Kommutator(list[i], list[i+1]));
            }
            if (f2diederek.Count == 1)
            {
                if(f2diederek[0] == new F2DiederElem(new DiederElem(0, 0)))
                    return ered+1;
                else return ered+2;
            }
            return ReszFeloldas(f2diederek, ered + 1);
        }

        static F2DiederElem Beolvasas(string input)
        {
            string[] elemek = input.Split('+');
            DiederElem[] diederek = new DiederElem[elemek.Length];
            for (int i = 0; i < elemek.Length; i++)
            {
                uint a = 1;
                if (elemek[i].Contains("a"))
                {
                    if (elemek[i].Contains("b"))
                    {
                        //Console.WriteLine(elemek[i].Substring(1, elemek[i].IndexOf("b") - 1));
                        string temp = elemek[i].Substring(1, elemek[i].IndexOf("b") - 1);
                        a = temp==""?1: uint.Parse(temp);
                        diederek[i] = new DiederElem(a, 1);
                    }
                    else if (elemek[i].Length > 1)
                    {
                        a = uint.Parse(elemek[i].Substring(1));
                        diederek[i] = new DiederElem(a, 0);
                    }
                    else diederek[i] = new DiederElem(a, 0);
                }
                else
                {
                    if (elemek[i].Contains("b")) diederek[i] = new DiederElem(0, 1);
                    else diederek[i] = new DiederElem(0, 0);
                }
            }
            return new F2DiederElem(diederek);
        }
        static Random r = new Random();
        static F2DiederElem RandomElem()
        {
            
            DiederElem[] diederek = new DiederElem[r.Next(1,(int)DiederElem.CSOPORT_RENDJE/2)*2-1];
            int i = 0;
            while (i < diederek.Length)
            {
                DiederElem temp = new DiederElem((uint)r.Next(0,(int)Math.Pow(2,(int)DiederElem.N-1)), (byte)r.Next(0, 2));
                if (!diederek.Contains(temp))
                {
                    diederek[i] = temp;
                    i++;
                }
            }
            return new F2DiederElem(diederek);
        }

        static int ReszFeloldas(F2DiederElem a, F2DiederElem b, F2DiederElem c, int ered)
        {
            
            F2DiederElem ic = a.Inverz * b.Inverz*a*b;
            F2DiederElem ib = a.Inverz * c.Inverz * a * c;
            F2DiederElem ia = b.Inverz * c.Inverz * b * c;
            if ((ia.Equals(new F2DiederElem(new DiederElem(0,0)))/*||ia.Equals(new F2DiederElem())*/)
                && (ib.Equals(new F2DiederElem(new DiederElem(0, 0)))/* || ib.Equals(new F2DiederElem())*/)
                && (ic.Equals(new F2DiederElem(new DiederElem(0, 0)))/* || ic.Equals(new F2DiederElem())*/))
            {
                return ered+1;
            }
            return ReszFeloldas(ia, ib, ic, ered + 1);
        }

        static void Main(string[] args)
        {
            DiederCsoport csoport = new DiederCsoport(DiederElem.CSOPORT_RENDJE);
            DateTime start;
            do
            {
                Console.Clear();

                //1. lehetőség
                Console.WriteLine("3 elemet vagy 6 elemet adna meg? (a számmal válaszoljon)");
                int szam = int.Parse(Console.ReadLine());
                if ( szam== 3)
                {
                    Console.WriteLine("3 elemet tárolunk el, és számoljuk ki részhalmazának feloldási hosszát");
                    Console.WriteLine("Írja be az első elemet! Formátum: 1+b+a+ab+a2+a2b+a3");
                    F2DiederElem a = Beolvasas(Console.ReadLine()/*"1+b+a+ab+a2+a2b+a3"*/);
                    Console.WriteLine("Írja be a második elemet!");
                    F2DiederElem b = Beolvasas(Console.ReadLine() /*"b+a2b+a3b"*/);
                    Console.WriteLine("Írja be a harmadik elemet!");
                    F2DiederElem c = Beolvasas(Console.ReadLine() /*"1+b+a2+a2b+a3"*/);
                    start = DateTime.Now;
                    Console.WriteLine("A részfeloldás hossza: {0}", ReszFeloldas(a, b, c, 0));

                    Console.WriteLine($"Futási idő: {(DateTime.Now - start).ToString(@"hh\:mm\:ss\.ffffff")}");
                }
                else if (szam == 6)
                {
                    Console.WriteLine("Írja be az első elemet! Formátum: 1+b+a+ab+a2+a2b+a3");
                    F2DiederElem seged= Beolvasas(Console.ReadLine());
                    Console.WriteLine("Írja be az elem párját!");
                    F2DiederElem a= Beolvasas(Console.ReadLine() ) ;
                    a = Kommutator(seged, a);
                    Console.WriteLine("Írja be az első elemet! Formátum: 1+b+a+ab+a2+a2b+a3");
                    seged = Beolvasas(Console.ReadLine());
                    Console.WriteLine("Írja be az elem párját!");
                    F2DiederElem b = Beolvasas(Console.ReadLine());
                    b=Kommutator(seged, b);
                    Console.WriteLine("Írja be az első elemet! Formátum: 1+b+a+ab+a2+a2b+a3");
                    seged = Beolvasas(Console.ReadLine());
                    Console.WriteLine("Írja be az elem párját!");
                    F2DiederElem c = Beolvasas(Console.ReadLine());
                    c= Kommutator(seged, c);
                    start = DateTime.Now;
                    Console.WriteLine("A részfeloldás hossza: {0}", ReszFeloldas(a, b, c, 1));
                    Console.WriteLine($"Futási idő: {(DateTime.Now - start).ToString(@"hh\:mm\:ss\.ffffff")}");
                }
                Console.WriteLine("Újra akarja csinálni? Ha nem, nyomjon ENTER-t, ha igen, írjon be valamit, és utána nyomjon ENTER-t");
            } while (Console.ReadLine() != "");
            //2. lehetőség
            Console.WriteLine("Mennyi a legnagyobb várható eredmény?");
            int maxered=int.Parse(Console.ReadLine());
            int ered = 0;
            Console.WriteLine("Akarja a random elem generátort használni? (igen/nem)");
            if(Console.ReadLine()=="igen")
            {
                long szamlal = 1;
                while (ered < maxered)
                {
                    start = DateTime.Now;
                    Console.WriteLine("{0}. random megoldás", szamlal);
                    ered = ReszFeloldas(RandomElem(), RandomElem(), RandomElem(), 0);
                    Console.WriteLine("Az eredmény jelenleg: {0}", ered);

                    Console.WriteLine($"Futási idő: {(DateTime.Now - start).ToString(@"hh\:mm\:ss\.ffffff")}");
                    Console.WriteLine("\n");
                    szamlal++;
                }

            }

            //3. lehetőség
            Console.WriteLine("Akarja a teljes csoport feloldását? (igen/nem)");
            if (Console.ReadLine() == "igen")
            {
                //Kell nekünk egy számláló, ami segít az adott sorszámú elemet legenerálni
                F2D2nCsoportElem x = new F2D2nCsoportElem();
                F2D2nCsoportElem y = new F2D2nCsoportElem();
                F2D2nCsoportElem z = new F2D2nCsoportElem();

                while (ered != maxered)
                {
                    ered = ReszFeloldas(x.JelenlegiElem(), y.JelenlegiElem(), z.JelenlegiElem(), 0);
                    //Console.WriteLine($"z elemszám: {z.poziciok.Count}\tz={z}");
                    if (!z.KovetkezoElem())
                    {
                        //Console.WriteLine($"y={y}");
                        z = new F2D2nCsoportElem();
                        if (!y.KovetkezoElem())
                        {
                            //Console.WriteLine($"x={x}");
                            y = new F2D2nCsoportElem();
                            if (!x.KovetkezoElem())
                            {
                                Console.WriteLine("Nem elérhető a maximális várható eredmény");
                                break;
                            }
                        }
                    }
                }
                if (ered == maxered)
                {
                    Console.WriteLine("Sikeres lefutás, eredmény a várható eredmény");
                }
            }

            //4. lehetőség
            int szamlalo = 1;
            while (ered != maxered)
            {
                List<F2DiederElem> f2diederek = new List<F2DiederElem>();
                for (int i = 0; i < Math.Pow(2, maxered - 2
                    ); i++)
                {
                    F2DiederElem a = RandomElem();
                    F2DiederElem b = RandomElem();
                    if (Kommutator(a, b) != new F2DiederElem(new DiederElem(0, 0)))
                    {
                        f2diederek.Add(a);
                        f2diederek.Add(b);
                    }
                }
                ered = ReszFeloldas(f2diederek, 0);
                Console.WriteLine("A {0}. részfeloldás eredménye: {1}", szamlalo, ered);
                szamlalo++;
            }
            
        }
    }
}
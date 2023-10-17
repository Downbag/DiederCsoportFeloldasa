using System.Reflection;

namespace AlgebraApp
{
    internal class Program
    {
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
                        Console.WriteLine(elemek[i].Substring(1, elemek[i].IndexOf("b") - 1));
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

            
            DateTime start= DateTime.Now;

            //1. lehetőség

            Console.WriteLine("3 elemet tárolunk el, és számoljuk ki részhalmazának feloldási hosszát");
            Console.WriteLine("Írja be az első elemet!");
            F2DiederElem a = Beolvasas(/*Console.ReadLine()*/"1+b+a+ab+a2+a2b+a3");
            Console.WriteLine("Írja be a második elemet!");
            F2DiederElem b = Beolvasas(/*Console.ReadLine()*/"b+a2b+a3b");
            Console.WriteLine("Írja be a harmadik elemet!");
            F2DiederElem c = Beolvasas(/*Console.ReadLine()*/"1+b+a2+a2b+a3");

            Console.WriteLine("A részfeloldás hossza: {0}", ReszFeloldas(a, b, c, 0));

            Console.WriteLine($"Futási idő: {(DateTime.Now - start).ToString(@"hh\:mm\:ss\.ffffff")}");
            //2. lehetőség
            Console.WriteLine("Mennyi a legnagyobb várható eredmény?");
            int maxered=int.Parse(Console.ReadLine());
            int ered = 0;
            Console.WriteLine("Akarja a random elem generátort használni? (igen/nem)");
            if(Console.ReadLine()=="igen")
            {
                long szamlalo = 1;
                while (ered < maxered)
                {
                    start = DateTime.Now;
                    Console.WriteLine("{0}. random megoldás", szamlalo);
                    ered = ReszFeloldas(RandomElem(), RandomElem(), RandomElem(), 0);
                    Console.WriteLine("Az eredmény jelenleg: {0}", ered);

                    Console.WriteLine($"Futási idő: {(DateTime.Now - start).ToString(@"hh\:mm\:ss\.ffffff")}");
                    Console.WriteLine("\n");
                    szamlalo++;
                }

            }

            //3. lehetőség
            //Kell nekünk egy számláló, ami segít az adott sorszámú elemet legenerálni
            F2D2nCsoportElem x = new F2D2nCsoportElem();
            F2D2nCsoportElem y = new F2D2nCsoportElem();
            F2D2nCsoportElem z = new F2D2nCsoportElem();
            
            while (ered != maxered)
            {
                ered= ReszFeloldas(x.JelenlegiElem(),y.JelenlegiElem(),z.JelenlegiElem(),0);
                if (!z.KovetkezoElem())
                {
                    z=new F2D2nCsoportElem();
                    if (!y.KovetkezoElem())
                    {
                        y=new F2D2nCsoportElem();
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
    }
}
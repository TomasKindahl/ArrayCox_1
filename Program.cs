using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;

using ds;

namespace ArrayCox_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter writer = new(@"stat.html"))
            {
                Random randomizer = new Random();

                // Stopwatches for array:
                Stopwatch arrayAppend = new Stopwatch();
                Stopwatch arraySet = new Stopwatch();
                Stopwatch arrayGet = new Stopwatch();
                Stopwatch arrayInsert = new Stopwatch();
                Stopwatch arrayDelete = new Stopwatch();

                // Stopwatches for single linked lists:
                Stopwatch sllAppend = new Stopwatch();
                Stopwatch sllSet = new Stopwatch();
                Stopwatch sllGet = new Stopwatch();
                Stopwatch sllInsert = new Stopwatch();
                Stopwatch sllDelete = new Stopwatch();

                // Stopwatches for single linked lists with improvements:
                Stopwatch htAppend = new Stopwatch();
                Stopwatch htSet = new Stopwatch();
                Stopwatch htGet = new Stopwatch();
                Stopwatch htInsert = new Stopwatch();
                Stopwatch htDelete = new Stopwatch();

                Array<int> arr = new Array<int>(10);
                arr.Print();
                arr.Append(new int[] { 2, 3, 5, 7, 11 });
                arr.Print();
                arr.Set(2, 12);
                arr.Print();
                Console.WriteLine($"arr.Get(2) = {arr.Get(2)}");
                arr.InsertAt(2, 33);
                arr.Print();
                arr.DeleteAt(3);
                arr.Print();

                SLList<int> sll = new SLList<int>(10);
                sll.Print();
                sll.Append(new int[] { 2, 3, 5, 7, 11 });
                sll.Print();
                sll.Set(2, 12);
                sll.Print();
                Console.WriteLine($"sll.Get(2) = {sll.Get(2)}");
                sll.InsertAt(2, 33);
                sll.Print();
                sll.DeleteAt(3);
                sll.Print();

                HashTab<int> ht = new HashTab<int>(10, 10);
                ht.Print();
                ht.Append(new int[] { 2, 3, 5, 7, 11 });
                ht.Print();
                ht.Set(2, 12);
                ht.Print();
                Console.WriteLine($"ht.Get(2) = {ht.Get(2)}");
                ht.InsertAt(2, 33);
                ht.Print();
                ht.DeleteAt(3);
                ht.Print();

                GenHTML.Head(writer, "Array statistics");
                GenHTML.Write(writer, "<h1>Array statistics</h1>");

                GenHTML.SVGhead(writer);
                for (int i = 0; i < 10; i++)
                {
                    GenHTML.SVGrotTextAt(writer, $"{i * 10000}", 30 * i + 27, 363);
                }
                GenHTML.SVGrotTextAt(writer, "100000", 327, 361);

                string[] txt = { "App.", "Set", "Get", "Ins.", "Del." };
                for(int i = 0; i < txt.Length; i++) 
                    GenHTML.SVGtextAt(writer, txt[i], 18, 360-60*i);

                GenHTML.Scale scale = new GenHTML.Scale { X = 0.003, Y = 30, xoff = 30, yoff = 40 };

                for (int size = 2_000; size <= 10_000/*100_000*/; size += 2_000)
                {
                    /*********************/
                    /* Mätning av Append */
                    /*********************/
                    Console.WriteLine("---- Append ----");
                    Array<int> Aa = new Array<int>(size);
                    arrayAppend.Reset();
                    arrayAppend.Start();
                    for (int i = 0; i < size; i++)
                        Aa.Append(randomizer.Next());
                    arrayAppend.Stop();
                    Console.WriteLine($"Appending Array<int> {size} times took {arrayAppend.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "red", size, arrayAppend.Elapsed.TotalSeconds);

                    SLList<int> Sa = new SLList<int>(size);
                    sllAppend.Reset();
                    sllAppend.Start();
                    for (int i = 0; i < size; i++)
                        Sa.Append(randomizer.Next());
                    sllAppend.Stop();
                    Console.WriteLine($"Appending SLList<int> {size} times took {sllAppend.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "blue", size, sllAppend.Elapsed.TotalSeconds);

                    /******************/
                    /* Mätning av Set */
                    /******************/
                    double setOffSet = 2;
                    Console.WriteLine("---- Set ----");
                    Array<int> As = new Array<int>(size);
                    for (int i = 0; i < size; i++)
                    {
                        As.Append(randomizer.Next());
                    }
                    arraySet.Reset();
                    arraySet.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(size);
                        As.Set(index, randomizer.Next());
                    }
                    arraySet.Stop();
                    Console.WriteLine($"Setting Array<int> {size} times took {arraySet.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#FF4444", size, arraySet.Elapsed.TotalSeconds+ setOffSet);

                    SLList<int> Ss = new SLList<int>(size);
                    for (int i = 0; i < size; i++)
                    {
                        Ss.Append(randomizer.Next());
                    }
                    sllSet.Reset();
                    sllSet.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(size);
                        Ss.Set(index, randomizer.Next());
                    }
                    sllSet.Stop();
                    Console.WriteLine($"Setting SLList<int> {size} times took {sllSet.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#4444FF", size, sllSet.Elapsed.TotalSeconds + setOffSet);

                    /******************/
                    /* Mätning av Get */
                    /******************/
                    Console.WriteLine("---- Get ----");
                    Array<int> Ag = new Array<int>(size);
                    for (int i = 0; i < size; i++)
                    {
                        Ag.Append(randomizer.Next());
                    }
                    arrayGet.Reset();
                    arrayGet.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(size);
                        Ag.Get(index);
                    }
                    arraySet.Stop();
                    Console.WriteLine($"Getting from Array<int> {size} times took {arrayGet.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#FF8888", size, arrayGet.Elapsed.TotalSeconds + setOffSet*2);

                    SLList<int> Sg = new SLList<int>(size);
                    for (int i = 0; i < size; i++)
                    {
                        Sg.Append(randomizer.Next());
                    }
                    sllGet.Reset();
                    sllGet.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(size);
                        Sg.Get(index);
                    }
                    sllGet.Stop();
                    Console.WriteLine($"Getting from SLList<int> {size} times took {sllGet.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#8888FF", size, sllGet.Elapsed.TotalSeconds + setOffSet * 2);

                    /***********************/
                    /* Mätning av InsertAt */
                    /***********************/
                    Console.WriteLine("---- InsertAt ----");
                    Array<int> Ai = new Array<int>(size * 2);
                    for (int i = 0; i < size; i++)
                    {
                        Ai.Append(randomizer.Next());
                    }
                    arrayInsert.Reset();
                    arrayInsert.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(size);
                        Ai.InsertAt(index, randomizer.Next());
                    }
                    arrayInsert.Stop();
                    Console.WriteLine($"Inserting into Array<int> {size} times took {arrayInsert.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#FFAAAA", size, arrayInsert.Elapsed.TotalSeconds + setOffSet * 3);

                    SLList<int> Si = new SLList<int>(size * 2);
                    for (int i = 0; i < size; i++)
                    {
                        Si.Append(randomizer.Next());
                    }
                    sllInsert.Reset();
                    sllInsert.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(size);
                        Si.InsertAt(index, randomizer.Next());
                    }
                    sllInsert.Stop();
                    Console.WriteLine($"Inserting into SLList<int> {size} times took {sllInsert.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#AAAAFF", size, sllInsert.Elapsed.TotalSeconds + setOffSet * 3);

                    /***********************/
                    /* Mätning av DeleteAt */
                    /***********************/
                    Console.WriteLine("---- DeleteAt ----");
                    Array<int> Ad = new Array<int>(size * 2);
                    for (int i = 0; i < size * 2; i++)
                    {
                        Ad.Append(randomizer.Next());
                    }
                    arrayDelete.Reset();
                    arrayDelete.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(Ad.Length());
                        Ad.DeleteAt(index);
                    }
                    arrayDelete.Stop();
                    Console.WriteLine($"Deleting from Array<int> {size} times took {arrayDelete.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#FFCCCC", size, arrayDelete.Elapsed.TotalSeconds + setOffSet * 4);

                    SLList<int> Sd = new SLList<int>(size * 2);
                    for (int i = 0; i < size * 2; i++)
                    {
                        Sd.Append(randomizer.Next());
                    }
                    sllDelete.Reset();
                    sllDelete.Start();
                    for (int i = 0; i < size; i++)
                    {
                        int index = randomizer.Next(Sd.Length());
                        Sd.DeleteAt(index);
                    }
                    sllDelete.Stop();
                    Console.WriteLine($"Deleting from SLList<int> {size} times took {sllDelete.Elapsed.TotalSeconds} seconds");
                    GenHTML.PointAt(writer, scale, "#CCCCFF", size, sllDelete.Elapsed.TotalSeconds + setOffSet * 4);

                    Console.WriteLine("================================");
                }
                GenHTML.SVGfoot(writer);

                GenHTML.Foot(writer);
            }
        }
    }
}

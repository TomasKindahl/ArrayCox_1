using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

using ds;

namespace ArrayCox_1
{
    class Program
    {
        static void Main(string[] args)
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

            for(int size = 10_000; size <= 100_000/*100_000*/; size += 10_000)
            {
                // Mätning av append:
                Array<int> A = new Array<int>(size);
                arrayAppend.Reset();
                arrayAppend.Start();
                for (int i = 0; i < size; i++)
                    A.Append(randomizer.Next());
                arrayAppend.Stop();
                Console.WriteLine($"Appending Array<int> {size} times took {arrayAppend.Elapsed.TotalSeconds} seconds");

                SLList<int> S = new SLList<int>(size);
                sllAppend.Reset();
                sllAppend.Start();
                for (int i = 0; i < size; i++)
                    S.Append(randomizer.Next());
                sllAppend.Stop();
                Console.WriteLine($"Appending SLList<int> {size} times took {sllAppend.Elapsed.TotalSeconds} seconds");

                // Mätning av Set (typ size/10 Set-ningar):

                // Mätning av Get (typ size/10 Get-ningar):

                // Mätning av InsertAt (typ size/10 InsertAt-ningar):

                // Mätning av DeleteAt (typ size/10 DeleteAt-ningar):
            }
        }
    }
}

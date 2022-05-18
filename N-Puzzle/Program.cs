using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace N_Puzzle
{
    class Program
    {
        static int DISTANCE_TYPE;
        static bool solve;
        static void Main(string[] args)
        {
            Console.WriteLine("SampleTest [1] : \nCompleteTest [2] : \nVeryLargeTest[3] :");
            int diff = int.Parse(Console.ReadLine());
            Console.WriteLine("Hamming [0] : \nManhattan [1] :");
            DISTANCE_TYPE = int.Parse(Console.ReadLine());
            switch (diff)
            {
                case 1 :
                    SampleTest();
                    break;
                case 2:
                    CompleteTest();
                    break;
                default:
                    VeryLargeTest();
                    break;
            }
        }

        static void SampleTest()
        {
            Console.WriteLine("Sample : ");
            
            string[] samplefilenames = { "8 Puzzle (1)", "8 Puzzle (2)", "8 Puzzle (3)", "15 Puzzle - 1", "24 Puzzle 1", "24 Puzzle 2" };

            foreach (string name in samplefilenames)
            {
                string solvedSample = @"Sample Test/Solvable Puzzles/" + name + @".txt";
                NPuzzle nPuzzle = ReadFile(solvedSample, DISTANCE_TYPE);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                if (solve)
                {
                    Node parent = nPuzzle.solve();
                    if (nPuzzle.n == 3)
                        nPuzzle.printroot(parent);
                    Console.WriteLine("steps :" + parent.depth);
                    watch.Stop();
                    Console.WriteLine($"Execution time : {watch.Elapsed.TotalSeconds} sec ");
                    Console.WriteLine();
                }
            }
            string[] samplefilenamesnot = { "8 Puzzle - Case 1", "8 Puzzle(2) - Case 1", "8 Puzzle(3) - Case 1", "15 Puzzle - Case 2", "15 Puzzle - Case 3" };
            foreach (string name in samplefilenamesnot)
            {
                string unsolvedSample = @"Sample Test/Unsolvable Puzzles/" + name + @".txt";
                ReadFile(unsolvedSample);
                Console.WriteLine();
            }
            
        }

        static void CompleteTest()
        {
            Console.WriteLine("Complete : ");
    
                string[] completefilenames = { "15 Puzzle 1", "15 Puzzle 3", "15 Puzzle 4", "15 Puzzle 5" };
                string[] completefilenames2 = { "50 Puzzle", "99 Puzzle - 1", "99 Puzzle - 2", "9999 Puzzle" };
                
                foreach (string name in completefilenames)
                {
                    string solvedComplete = @"Complete Test/Solvable puzzles/Manhattan Only/" + name + @".txt";
                    NPuzzle nPuzzle =  ReadFile(solvedComplete, 1);
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    if (solve)
                    {
                        Node parent = nPuzzle.solve();
                        if (nPuzzle.n == 3)
                            nPuzzle.printroot(parent);
                        Console.WriteLine("steps :" + parent.depth);
                        watch.Stop();
                        Console.WriteLine($"Execution time : {watch.Elapsed.TotalSeconds} sec ");
                        Console.WriteLine();
                    }
                }

                foreach (string name in completefilenames2)
                {
                    string solvedComplete = @"Complete Test/Solvable puzzles/Manhattan & Hamming/" + name + @".txt";
                    NPuzzle nPuzzle = ReadFile(solvedComplete, DISTANCE_TYPE);
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    if (solve)
                    {
                        Node parent = nPuzzle.solve();
                        if (nPuzzle.n == 3)
                            nPuzzle.printroot(parent);
                        Console.WriteLine("steps :" + parent.depth);
                        watch.Stop();
                        Console.WriteLine($"Execution time : {watch.Elapsed.TotalSeconds} sec ");
                        Console.WriteLine();
                    }
                }

                string[] completefilenamesnot = { "15 Puzzle 1 - Unsolvable", "99 Puzzle - Unsolvable Case 1", "99 Puzzle - Unsolvable Case 2", "9999 Puzzle" };

                foreach (string name in completefilenamesnot)
                {
                    string unsolvedComplete = @"Complete Test/Unsolvable puzzles/" + name + @".txt";
                    ReadFile(unsolvedComplete);
                    Console.WriteLine();
                }
            
        }

        static void VeryLargeTest()
        {
            string veryLarge = @"Complete Test/V. Large test case/TEST.txt";

            NPuzzle nPuzzle = ReadFile(veryLarge, 1);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (solve)
            {
                Node parent = nPuzzle.solve();
                if (nPuzzle.n == 3)
                    nPuzzle.printroot(parent);
                Console.WriteLine("steps :" + parent.depth);
                watch.Stop();
                Console.WriteLine($"Execution time : {watch.Elapsed.TotalSeconds} sec ");
                Console.WriteLine();
            }
        }

        static NPuzzle ReadFile(string fileName, int methodType = 1)
        {
            List<int> lis = new List<int>();
            string[] s = File.ReadAllLines(fileName);
            int n = int.Parse(s[0]);
            NPuzzle nPuzzle = new NPuzzle(n, methodType);
            int c = 0;
            int i = 0;
            if (s[1] == "") i = 2;
            else i = 1;
            for (; i < s.Length; i++)
            {
                string[] k = s[i].Split(' ');
                for (int j = 0; j < k.Length; j++)
                {
                    if (!k[j].Equals(""))
                    {
                        int result = 0;
                        int.TryParse(k[j], out result);
                        lis.Add(result);
                        nPuzzle.matrix[c, j] = result;
                        if (nPuzzle.matrix[c, j] == 0)
                        {
                            nPuzzle.x0 = c;
                            nPuzzle.y0 = j;
                        }
                    }
                }
                c++;
            }
            solve = nPuzzle.isSolvable(lis);
            Console.WriteLine( solve ? "Solvable" : "not Solvable");
            return nPuzzle;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace N_Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("SampleTest [1] : \nCompleteTest [2] : \nVeryLargeTest[3] :");
            int diff = int.Parse(Console.ReadLine());
            

            switch (diff)
            {
                case 1 :
		    Console.WriteLine("Solvable[1] : \nNotSolvable[2] :");
		    int type = int.Parse(Console.ReadLine());
                    SampleTest(type);
                    break;
                case 2:
                    Console.WriteLine("Solvable[1] : \nNotSolvable[2] :");
            	    int type = int.Parse(Console.ReadLine());
                    CompleteTest(type);
                    break;
                default:
                    VeryLargeTest();
                    break;
            }
        }


        static void SampleTest(int type)
        {
            Console.WriteLine("Sample : ");
            if (type == 1)
            {
                string[] samplefilenames = { "8 Puzzle (1)", "8 Puzzle (2)", "8 Puzzle (3)", "15 Puzzle - 1", "24 Puzzle 1", "24 Puzzle 2" };
               
                foreach (string name in samplefilenames)
                {
                    string solvedSample = @"Sample Test/Solvable Puzzles/" + name + @".txt";
                    NPuzzle nPuzzle = ReadFile(solvedSample, 0);
                    nPuzzle.solve();
                    Console.WriteLine();

                }
            }
            else
            {
                string[] samplefilenames = { "8 Puzzle - Case 1", "8 Puzzle(2) - Case 1", "8 Puzzle(3) - Case 1", "15 Puzzle - Case 2", "15 Puzzle - Case 3" };
                foreach (string name in samplefilenames)
                {
                    string unsolvedSample = @"Sample Test/Unsolvable Puzzles/" + name + @".txt";
                    NPuzzle nPuzzle = ReadFile(unsolvedSample, 0);
                    Console.WriteLine();
                }
            }
        }

        static void CompleteTest(int type)
        {
            Console.WriteLine("Complete : ");
            if (type == 1)
            {
                string[] completefilenames = { "15 Puzzle 1", "15 Puzzle 3", "15 Puzzle 4", "15 Puzzle 5" };
                string[] completefilenames2 = { "50 Puzzle", "99 Puzzle - 1", "99 Puzzle - 2", "9999 Puzzle" };
                
                foreach (string name in completefilenames)
                {
                    string solvedComplete = @"Complete Test/Complete Test/Solvable puzzles/Manhattan Only/" + name + @".txt";
                    NPuzzle nPuzzle =  ReadFile(solvedComplete, 1);
                    nPuzzle.solve();
                    Console.WriteLine();
                }

                foreach (string name in completefilenames2)
                {
                    string solvedComplete = @"Complete Test/Complete Test/Solvable puzzles/Manhattan & Hamming/" + name + @".txt";
                    NPuzzle nPuzzle = ReadFile(solvedComplete, 0);
                    nPuzzle.solve();
                    Console.WriteLine();
                }
            }
            else
            {

                string[] completefilenames = { "15 Puzzle 1 - Unsolvable", "99 Puzzle - Unsolvable Case 1", "99 Puzzle - Unsolvable Case 2", "9999 Puzzle" };

                foreach (string name in completefilenames)
                {
                    string unsolvedComplete = @"Complete Test/Complete Test/Unsolvable puzzles/" + name + @".txt";
                    NPuzzle nPuzzle = ReadFile(unsolvedComplete, 0);
                    Console.WriteLine();
                }
            }
        }

        static void VeryLargeTest()
        {
            string veryLarge = @"Complete Test/Complete Test/V. Large test case/TEST.txt";

            NPuzzle nPuzzle = ReadFile(veryLarge, 1);
            nPuzzle.solve();
            Console.WriteLine();

        }

        static NPuzzle ReadFile(string fileName, int methodType)
        {
            List<int> lis = new List<int>();
            string[] s = File.ReadAllLines(fileName);
            int n = int.Parse(s[0]);
            NPuzzle nPuzzle = new NPuzzle(n, methodType);
            int c = 0;
            for (int i = 2; i < s.Length; i++)
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
            Console.WriteLine(nPuzzle.isSolvable(lis) ? "Solvable" : "not Solvable");
            return nPuzzle;
        }
    }
}

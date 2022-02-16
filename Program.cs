using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_homework_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // test Puzzle
            int[] puzzle =
            {
                1,3,4,
                8,0,5,
                7,2,6
            };


            node startNode = new node(puzzle); // create starting puzzleboard using the test puzzle

            Solvers BFS = new Solvers(); // creates a solver class for BFS

            Solvers DFS = new Solvers(); // creates a solver class for DFS

            Solvers UCS = new Solvers(); // creates a solver class for UCS
            Console.WriteLine( "\n\n"+
                "/////////////////////////////////////////////////" +
                "\n//////////    BFS Search Algorithm    ///////////" +
                "\n////////////////////////////////////////////////"
                );
            List<node> BFSsolution = BFS.BFS(startNode); // creates a list node variable to store results of a BFS search

            if (BFSsolution.Count > 0)
            {
                for (int i = BFSsolution.Count - 1; i >= 0; i--)
                {

                    if (BFSsolution.Count - 1 == i)
                    {
                        Console.WriteLine("\n  Printing solution, Starting location");
                    }
                    else if (i == 0)
                    {
                        Console.WriteLine("\n  Printing solution, Final position location");
                    }
                    else
                    {
                        Console.WriteLine("\n  Printing solution, NEXT location");
                    }
                    BFSsolution[i].printPuzzleBoard();
                }
            }
            else
            {
                Console.WriteLine("No path to solution is found");
            }



            Console.WriteLine("\n\n" +
                "/////////////////////////////////////////////////" +
                "\n//////////    DFS Search Algorithm    ///////////"+
                "\n////////////////////////////////////////////////"
                );
            List<node> DFSsolution = DFS.DFS(startNode, 6); // creates a list node variable to store results of a DFS search

            if (DFSsolution.Count>0)
            {
                for(int i= DFSsolution.Count-1; i>=0; i--)
                {
                    
                    if(DFSsolution.Count-1 == i)
                    {
                        Console.WriteLine("\n  Printing solution, Starting location");
                    }
                    else if(i==0)
                    {
                        Console.WriteLine("\n  Printing solution, Final position location");
                    }
                    else
                    {
                        Console.WriteLine("\n  Printing solution, NEXT location");
                    }
                    DFSsolution[i].printPuzzleBoard();
                }
            }
            else
            {
                Console.WriteLine("No path to solution is found");
            }


            Console.WriteLine("\n\n" +
               "/////////////////////////////////////////////////" +
               "\n//////////    UCS Search Algorithm    ///////////" +
               "\n////////////////////////////////////////////////"
               );
            List<node> UCSsolution = UCS.UCS(startNode); // creates a list node variable to store results of a UCS search

            if (UCSsolution.Count > 0)
            {
                for (int i = DFSsolution.Count - 1; i >= 0; i--)
                {

                    if (DFSsolution.Count - 1 == i)
                    {
                        Console.WriteLine("\n  nPrinting solution, Starting location");
                    }
                    else if (i == 0)
                    {
                        Console.WriteLine("\n  Printing solution, Final position location");
                    }
                    else
                    {
                        Console.WriteLine("\n  Printing solution, NEXT location");
                    }
                    DFSsolution[i].printPuzzleBoard();
                }
            }
            else
            {
                Console.WriteLine("No path to solution is found");
            }
            Console.Read();
        }
    }
}

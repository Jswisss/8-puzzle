using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_homework_1
{
    class node
    {

        public List<node> children = new List<node>();//Creates child node list
        public node parent;// create parent node for reference back tracing 
        public int[] puzzleboard = new int[9];// puzzle configuration this is where each puzzle board will be placed per look at
        public int x = 0;// x is a variable to tell where the open space is in the array
        public int col = 3;// represents number of columns in the array, used for left and right expandtions
        public int row = 3;// represents number of rows in the array, used for up and down expandtions
        public int[] GoalBoard = { 1, 2, 3,
                                   8, 0, 4, 
                                   7, 6, 5,
                                        };
        //GoalBoard is what the solvers are striving to find when running there searches

        public node(int[] p)
        {// starting method for configuring the puzzle array
            SetPuzzle(p);
        }

        public void SetPuzzle(int[]p)
        {
            for (int i=0; i< puzzleboard.Length; i++)
            {// set puzzle array up = to its inputs in the main
                puzzleboard[i] = p[i];
            }
        }

        public void ExpandParent()//expands parent node in order to find children
        {
            for( int i= 0; i< puzzleboard.Length; i++)
            {
                if(puzzleboard[i]==0)
                {//finds where the empty variable is AKA 0
                    x = i;
                }
            }

            MoveRight(puzzleboard, x); // expands right
            MoveLeft(puzzleboard, x); //expands left
            MoveUp(puzzleboard, x); // expand up
            MoveDown(puzzleboard, x); // expand down
        }

        public void MoveRight(int[] p, int i)
        {// checks to see if it is possible to move right
         if (i % col< col -1)
            {
                int[] board = new int[9];
                CopyBoard(board, p);
                //creates new board and copies parent
                int temp = board[i + 1];
                board[i + 1] = board[i];
                board[i] = temp;
                //moves values and swap position of zero
                node child = new node(board);
                children.Add(child);// adds board to our child list
                child.parent = this; // places board node as a child of the current puzzleboard being expanded
            }
        }
        public void MoveLeft(int[] p, int i)
        {// checks to see if it is possible to move left
            if (i % col > 0)
            {
                int[] board = new int[9];
                CopyBoard(board, p);
                //creates new board and copies parent
                int temp = board[i - 1];
                board[i - 1] = board[i];
                board[i] = temp;
                //moves values and swap position of zero
                node child = new node(board);
                children.Add(child);// adds board to our child list
                child.parent = this; // places board node as a child of the current puzzleboard being expanded
            }
        }
        public void MoveUp(int[] p, int i)
        {// checks to see if it is possible to move up
            if (i - row >=0)
            {
                int[] board = new int[9];
                CopyBoard(board, p);
                //creates new board and copies parent
                int temp = board[i - 3];
                board[i - 3] = board[i];
                board[i] = temp;
                //moves values and swap position of zero
                node child = new node(board);
                children.Add(child);// adds board to our child list
                child.parent = this; // places board node as a child of the current puzzleboard being expanded
            }
        }
        public void MoveDown(int[] p, int i)
        {// checks to see if it is possible to move down
            if (i + row <= 8)
            {
                int[] board = new int[9];
                CopyBoard(board, p);
                //creates new board and copies parent
                int temp = board[i + 3];
                board[i + 3] = board[i];
                board[i] = temp;
                //moves values and swap position of zero
                node child = new node(board);
                children.Add(child);// adds board to our child list
                child.parent = this; // places board node as a child of the current puzzleboard being expanded
            }
        }

        public void printPuzzleBoard()
        { // outputs puzzleboard when solution is found
            Console.WriteLine();
            int a = 0;
            for(int i = 0; i<col; i++)
            {//goes through column
                for(int j = 0; j<row; j++)
                {//goes throufh row
                    Console.Write("   " + puzzleboard[a] + " ");
                    a++;//prints variable at each array position
                }
                Console.WriteLine();
            }
        }

        public bool isInList(int[] p)
        {// bool to find out if the current array is already in our list or not
            bool sameBoard = true;
            for(int i = 0; i< p.Length; i++)
            {//runs through for loop and check to see if the current board is in the list or not
                if(puzzleboard[i] != p[i])
                {
                    sameBoard = false;
                }
            }
            return sameBoard;
        }

        public int howComplete()
        {// returns value of how many is currently correct in the puzzle
            //used for Priority queue to see where it needs to be added in the queue
            int isGoal = 0;

            for (int i = 0; i < puzzleboard.Length; i++)
            {//runs through for loop and sees how many variables the current puzzle has in common with the goal board
                if (GoalBoard[i] == puzzleboard[i])
                {
                    isGoal ++;// adds one to isGoal variable
                }

            }
            return isGoal;
        }

        public void CopyBoard(int[] i, int[] j)
        {//just copies the the variables from one array list to another
            //used for creating children
            for(int k = 0; k< j.Length; k++)
            {//runs through for loop and copies variable to other array
                i[k] = j[k];
            }
        }

        public bool isGoalReach()
        {//bool to see if the current puzzle board is equal to the goalBoard
            bool isGoal = true;

            for(int i = 0; i< puzzleboard.Length; i++)
            {//runs through for loop testing each variable to see if they are equal
                if (GoalBoard[i]!= puzzleboard[i])
                {
                    isGoal = false;
                 
                }

            }
            return isGoal;
        }
    }
}

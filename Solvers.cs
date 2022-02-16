using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AI_Assignment_homework_1
{

    
    class Solvers
    {
        public Solvers()
        {

        }
        
        public List<node> BFS(node root)// Uses BFS search to find goal solution
        {
            List<node> BFSSolution = new List<node>();//this is what is going to be returned when solution is found
            List<node> queueList = new List<node>();//this list is used to store nodes that have not been expanded on.
            //queueList funtions like a queue
            List<node> expandedList = new List<node>();// stores nodes that have been expanded

            queueList.Add(root);//adds the starting puzzle to the queue
            bool goal_state = false; // sets goal state to false when it finds a solution this will return true

            while(queueList.Count>0 && !goal_state)// while loop will fall out of it if there is nothing in the queuelist meaning it has no new nodes to look at
                //and if it founds a goal solution it will also fall out
            {
                node currentNode = queueList[0]; // sets currentnode to the first postition of the queue
                expandedList.Add(currentNode);//adds currentnode to out expanded list because it is about to be expanded on
                queueList.RemoveAt(0);// removes queueList because it is about to be expanded on

                currentNode.ExpandParent();// function that adds children to this node. Moves left. right, up and down on puzzle

                for(int i= 0; i< currentNode.children.Count; i++)// for loop to go through each child that was just created
                {
                    node currentChild = currentNode.children[i]; // sets each child to the currentChild node
                    if(currentChild.isGoalReach())// tests to see if it is the goal or not
                    {//if it is it will change the goal state to true and now pathtrace back to the starting puzzle
                        Console.WriteLine("Goal has been Located");
                        goal_state = true;
                        //trace path to root node
                        PathTrace(BFSSolution, currentChild);
                    }
                    //if the child is already in queuelist and the expanded list it will not be re-added this saves on space
                    //because we don't need duplicate puzzle start because then we are doing double the work
                    if (!Contains(queueList, currentChild) && !Contains(expandedList, currentChild))
                        queueList.Add(currentChild);// adds it to the queue list at the end if not in either
                    
                }
            }// returns to the top of the loop to start again at a new node to expand on.

            return BFSSolution;//reutrn solution if one is found
        }

        public List<node> DFS(node root, int maxdepth)//Uses DFS search to find solution
        {
            List<node> DFSSolution = new List<node>(); //this is what is going to be returned when solution is found
            List<node> stackList = new List<node>(); // create a stack node list to store children and parent nodes for the puzzle
            List<node> visitedList = new List<node>(); // is a node list to store nodes that have been visited and checked
            int currdepth = 0; // starting depth for the DFS search 
            int lastchild = 0; // int number that goes up if children are found on a parent node
            stackList.Add(root); // adds the root to the stack list
            bool goal_state = false; // sets goal state to false when change to true if solution is found
            
            while (stackList.Count > 0 && !goal_state)// while loop that goes on until there is nothing in the stack or goal state is found
            {
                node currentNode = stackList[0];// created current node node and sets it equal to the node at the top of the stack
                //had to declare it here because I ran into issues when it was located in another position

                while (currdepth<maxdepth && stackList.Count> 0 && !goal_state)// while loop that looks to see if the current depth equal max depth 
                {// and sees if there is still anything is left in the stackList
                    
                    currentNode = stackList[0];// sets current node equal to top node in the stack

                    if (stackList.Count > 1)//if the stack has more than one node in the list it
                    {
                        if (Contains(visitedList, stackList[0]) && Contains(visitedList, stackList[1]))
                        {//checks to see if both top and second top node in the stack list has been checked and add to the visited list
                         //if both are in the list then they are both removed and decreases current depth by one
                            stackList.RemoveAt(0);
                            stackList.RemoveAt(0);
                            currdepth--;
                            if(stackList.Count != 0)// if the stack List still has nodes in it the it wll set the current node to the top stack node
                            {
                                currentNode = stackList[0];
                            }
                        }
                        else if (Contains(visitedList, stackList[0]))
                        {//if just the top node has been visited then remove the node from the stack
                            stackList.RemoveAt(0);
                            currentNode = stackList[0]; // set currrent node to the top of the stack
                            currdepth--; // decrease current depth by one
                        }
                    }

                    if (stackList.Count != 0) // if there is something in the stack
                    {
                        if (currentNode.isGoalReach())
                        {// else check to see if the current node is equal to the goal state
                            Console.WriteLine("Goal has been Located");
                            goal_state = true;
                            //trace path to root node
                            PathTrace(DFSSolution, currentNode);
                        } 
                        if (Contains(visitedList, stackList[0]) && stackList.Count == 1)
                        {// if the stack is just the root and the root has been visited the it is removed
                            stackList.RemoveAt(0);

                        }
                            else
                        {
                            lastchild = 0; // set last child to zero
                            currentNode.ExpandParent(); // function that adds children to this node. Moves left. right, up and down on puzzle
                            for (int i = currentNode.children.Count - 1; i >= 0; i--)
                            {//a for loop that checks to see if the child is in the stack and visited lists
                                node currentChild = currentNode.children[i];
                                if (!Contains(stackList, currentChild) && !Contains(visitedList, currentChild))
                                {// if not add it to stacklist and increase lastchild by one
                                    stackList.Insert(0, currentChild);
                                    lastchild++;
                                }
                            }
                            if(lastchild!=0)
                            {// if the parent had a child the increase depth by one
                                currdepth++;
                            }

                        }
                        visitedList.Add(currentNode);// add current node to visitedList
                    }
                }
                //once it gets kicked out of the while loop for going to deep
                if(lastchild>0)
                {//if the current node has children
                    currentNode = stackList[0]; // sets currentnode to the top of the stack list
                    //visitedList.Add(currentNode);// adds it to the vistedList
                    stackList.RemoveAt(0);// Pops off the top node of the stack
                    lastchild--;// decrease last child by one
                    if (currentNode.isGoalReach())// checks to see if currentNode is the goal
                    {
                        Console.WriteLine("Goal has been Located");
                        goal_state = true;
                        //trace path to root node
                        PathTrace(DFSSolution, currentNode);
                    }
                    if (lastchild == 0)
                    {//Once it goes through each child of a parent node it decrease the currdepth by one
                        currdepth--;
                    }
                }

            }
            return DFSSolution;//returns DFS solution
        }

        public List<node> UCS(node root)// Uses UCS search to find goal solution
        {
            List<node> UCSSolution = new List<node>();//this is what is going to be returned when solution is found
            PriorityQueue<node> queueList = new PriorityQueue<node>();
            List<node> expandedList = new List<node>();// stores nodes that have been expanded
            int totalcorrect = 0;
            totalcorrect = root.howComplete();
            queueList.Enqueue(totalcorrect, root );//adds the starting puzzle to the queue
            bool goal_state = false; // sets goal state to false when it finds a solution this will return true

            while (queueList.Count > 0 && !goal_state)// while loop will fall out of it if there is nothing in the queuelist meaning it has no new nodes to look at
                                                      //and if it founds a goal solution it will also fall out
            {
                node currentNode = queueList.Dequeue(); // sets currentnode to the first postition of the queue
                // dequueue queueList because it is about to be expanded on
                expandedList.Add(currentNode);
                currentNode.ExpandParent();// function that adds children to this node. Moves left. right, up and down on puzzle
                
                for (int i = 0; i < currentNode.children.Count; i++)// for loop to go through each child that was just created
                {
                    node currentChild = currentNode.children[i]; // sets each child to the currentChild node
                    if (currentChild.isGoalReach() && goal_state == false)// tests to see if it is the goal or not
                    {//if it is it will change the goal state to true and now pathtrace back to the starting puzzle
                        Console.WriteLine("Goal has been Located");
                        goal_state = true;
                        //trace path to root node
                        PathTrace(UCSSolution, currentChild);
                    }
                    //if the child is already in queuelist and the expanded list it will not be re-added this saves on space
                    //because we don't need duplicate puzzle starts because then we are doing double the work
                    if (!queueList.isInQueue(currentChild) && !Contains(expandedList, currentChild))// checks to see if the child is already in the queue
                    {
                        totalcorrect = currentChild.howComplete();// returns and sets the number of how many the current child has right
                        queueList.Enqueue(totalcorrect, currentChild);// adds current child to the queue list at th
                    } 

                }
            }// returns to the top of the loop to start again at a new node to expand on.

            return UCSSolution;
        }


        public void PathTrace(List<node> path, node n)
        {//traces path from the current node back to the root node and adds it to a new list
            Console.WriteLine("Tracing path...");
            node current = n;

            path.Add(current);

            while (current.parent != null)
            {
                current = current.parent;
                path.Add(current);
            }
        }

        public static bool Contains(List<node> list, node c)
        {// see if the current node is in a particular list
            bool contains = false;
            for(int i = 0; i<list.Count; i++)
            {
                if(list[i].isInList(c.puzzleboard))
                {
                    contains = true;
                }
            }
            return contains;
        }
    }
}

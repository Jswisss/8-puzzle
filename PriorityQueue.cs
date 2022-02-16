using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_homework_1
{
    public class PriorityQueue<T>
    {
        class Node
        { // Creates a node class that stores the priority number and puzzleboard
            public int Priority { get; set; }
            public T Object { get; set; }
        }

        List<Node> queue = new List<Node>(); // creates a list to store each puzzleboard
        int heapsize = -1;// sets starting heao size
        bool _isMinPriorityQueue;// creates bool that will be used in other method funtions
        //used to see if we are min or max queue
        public int Count { get { return queue.Count; } }// creates count variable


        public PriorityQueue(bool isMinPriorityQueue = false)
        {// see if we are min queueing or max queueing
            _isMinPriorityQueue = isMinPriorityQueue;
        }

        public void Enqueue(int priority, T obj)
        {//add puzzle board to our queue list with a priority
            Node node = new Node()
            {
                Priority = priority,
                Object = obj
            };

            queue.Add(node);
            heapsize++;
            if(_isMinPriorityQueue)//sorts the priority queue based on what heap is meaning used
            {
                BuildHeapMin(heapsize);
            }
            else
            {
                BuildHeapMax(heapsize);
            }
        }

        public T Dequeue()
        {// remove the first puzzle board from the priority queue
            if(heapsize>-1)
            {
                var returnVal = queue[0].Object;
                queue[0] = queue[heapsize];
                queue.RemoveAt(heapsize);
                heapsize--;
                if(_isMinPriorityQueue)
                {
                    MinHeapify(0);
                }
                else
                {
                    MaxHeapify(0);
                }
                return returnVal;
            }
            else
            {
                throw new Exception("Queue is empty");
            }
        }

        void UpdatePriority(T obj, int priority)
        {//updates priority of a puzzle board if needed
            
            for(int i = 0; i<= heapsize; i++)
            {
                Node node = queue[i];
                if(object.ReferenceEquals(node.Object, obj))
                {
                    node.Priority = priority;
                    if(_isMinPriorityQueue)
                    {
                        BuildHeapMin(i);
                        MinHeapify(i);
                    }
                    else
                    {
                        BuildHeapMax(i);
                        MaxHeapify(i);
                    }
                }
            }
        }

        public bool isInQueue (T obj)
        {// see if the current puzzle board is in the priority queue and returns a bool
            foreach( Node node in queue)
                if(object.ReferenceEquals(node.Object, obj))
                {
                    return true;
                }
            return false;
        }

        private void BuildHeapMax(int i)
        {// This will maintain the max heap of the priority queue so highest at first node position
            while(i>= 0 && queue[(i-1)/2].Priority < queue[i].Priority)
            {
                swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        private void BuildHeapMin(int i)
        {// This will maintain the min heap of the priority queue so lowest at first node position
            while (i >= 0 && queue[(i - 1) / 2].Priority > queue[i].Priority)
            {
                swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        private void MaxHeapify(int i)
        {// used to orgainize the max heap
            int left = ChildL(i);
            int right = ChildR(i);

            int highest = i;

            if(left<= heapsize && queue[highest].Priority< queue[left].Priority)
            {
                highest = left;
            }

            if(right<= heapsize && queue[highest].Priority <queue[right].Priority)
            {
                highest = right;
            }
            if(highest != i)
            {
                swap(highest, i);
                MaxHeapify(highest);
            }
        }

        private void MinHeapify(int i)
        {// used to orgainize the min heap
            int left = ChildL(i);
            int right = ChildR(i);

            int Lowest = i;

            if (left <= heapsize && queue[Lowest].Priority > queue[left].Priority)
            {
                Lowest = left;
            }

            if (right <= heapsize && queue[Lowest].Priority > queue[right].Priority)
            {
                Lowest = right;
            }
            if (Lowest != i)
            {
                swap(Lowest, i);
                MaxHeapify(Lowest);
            }
        }

        private void swap (int i, int j)
        {// swap two puzzle boards. used for when we are dequeue a puzzle board
            var temp = queue[i];
            queue[i] = queue[j];
            queue[j] = temp;
        }

        private int ChildL(int i)
        {// returning left child of the heap
            return i * 2 + 1;

        }

        private int ChildR(int i)
        {//returning right child of the heap
            return i * 2 + 2;
        }
    }
}

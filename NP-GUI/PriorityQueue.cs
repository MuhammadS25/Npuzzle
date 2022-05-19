using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class PriorityQueue
    {
        int size = 0;
        List<Node> items = new List<Node>();
        public PriorityQueue()
        {
            items = new List<Node>();
        }

        public void push(Node value)// log(V)
        {
            items.Add(value);
            size++;
            int index = size - 1;
            while (index >= 0 && items[(index - 1) / 2].hValue > items[index].hValue)//log(V)
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public Node pop() //log(V)
        {
            Node value = items[0];
            items[0] = items[size - 1];
            items.RemoveAt(size - 1); //O(1)
            size--;
            heapifyMin(0); // log(V)
            return value;
        }

        private void heapifyMin(int index) // log(V)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;

            int highest = index;

            if (left < size && items[highest].hValue > items[left].hValue)
                highest = left;
            if (right < size && items[highest].hValue > items[right].hValue)
                highest = right;

            if (highest != index)
            {
                Swap(highest, index);
                heapifyMin(highest);
            }
        }

        void Swap(int first, int second)//Θ(1)
        {
            Node temp = items[first];
            items[first] = items[second];
            items[second] = temp;
        }

        public int getSize() { return size; }//Θ(1)
    }
}

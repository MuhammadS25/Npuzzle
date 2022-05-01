using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class PriorityQueue { 
        
        int size = 0;
        List<Node> elements = new List<Node>();
        public PriorityQueue()
        {
            elements = new List<Node>();
        }

        public void push(Node value)
        {
            elements.Add(value);
            size++;
            int index = size -1 ;
            while (index >= 0 && elements[(index - 1) / 2].hValue > elements[index].hValue)
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public Node pop()
        {
            Node value = elements[0];
            elements[0] = elements[size-1];
            elements.RemoveAt(size-1);
            size--;
            heapifyMin(0);
            return value;
        }

        private void heapifyMin(int index)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;

            int highest = index;

            if (left < size && elements[highest].hValue > elements[left].hValue)
                highest = left;
            if (right < size && elements[highest].hValue > elements[right].hValue)
                highest = right;

            if (highest != index)
            {
                Swap(highest, index);
                heapifyMin(highest);
            }
        }

        void Swap(int first , int second)
        {
            Node temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }

        public int getSize() { return size ; }
    }
}

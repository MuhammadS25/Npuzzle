using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class PriorityQueue { 
        int size = 0;
        List<Node> items = new List<Node>();
        public PriorityQueue()
        {
            items = new List<Node>();
        }

        public void push(Node value)
        {
            items.Add(value);
            size++;
            int index = size - 1 ;
            while (index >= 0 && items[(index - 1) / 2].hValue > items[index].hValue)
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public Node pop()
        {
            Node value = items[0];
            items[0] = items[size-1];
            items.RemoveAt(size-1);
            size--;
            heapifyMin(0);
            return value;
        }

        private void heapifyMin(int index)
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

        void Swap(int first , int second)
        {
            Node temp = items[first];
            items[first] = items[second];
            items[second] = temp;
        }

        public int getSize() { return size ; }
    }
}

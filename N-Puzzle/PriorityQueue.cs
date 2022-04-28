using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class PriorityQueue { 
        List<int> elements = new List<int>();
        int size = -1;
        
        public PriorityQueue()
        {
            elements = new List<int>();
        }

        public void push(int value)
        {
            elements.Add(value);
            size++;
            int index = size;
            while (index >= 0 && elements[(index - 1) / 2] > elements[index])
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public int pop()
        {
            int value = elements[0];
            elements[0] = elements[size];
            elements.RemoveAt(size);
            size--;
            heapifyMin(0);
            return value;
        }

        private void heapifyMin(int index)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;

            int highest = index;

            if (left <= size && elements[highest] > elements[left])
                highest = left;
            if (right <= size && elements[highest] > elements[right])
                highest = right;

            if (highest != index)
            {
                Swap(highest, index);
                heapifyMin(highest);
            }
        }

        void Swap(int first , int second)
        {
            int temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }

        public int getSize() { return size ; }
    }
}

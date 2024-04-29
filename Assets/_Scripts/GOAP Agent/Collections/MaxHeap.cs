using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MaxHeap<T> : Heap<T>
{
    public MaxHeap(Func<T, T, bool> Compare) : base(Compare)
    {
    }

    protected override void ReCalculateDown(Func<T, T, bool> Compare)
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            var smallerIndex = GetLeftChildIndex(index);
            if (HasRightChild(index) && Compare(GetRightChild(index), GetLeftChild(index)))
            {
                smallerIndex = GetRightChildIndex(index);
            }

            if (Compare(_elements[smallerIndex],_elements[index]) == false)
            {
                break;
            }

            Swap(smallerIndex, index);
            index = smallerIndex;
        }
    }

    protected override void ReCalculateUp(Func<T, T, bool> Compare)
    {
        var index = _elements.Count - 1;
        while (!IsRoot(index) && Compare(_elements[index], GetParent(index)))
        {
            var parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }

}


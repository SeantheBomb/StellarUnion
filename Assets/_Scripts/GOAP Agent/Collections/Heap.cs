using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Heap<T> 
{

    public int Count => _elements.Count;


    protected readonly List<T> _elements;
    //protected int _size;

    Func<T, T, bool> Compare;

    public Heap(Func<T, T, bool> Compare)
    {
        _elements = new List<T>();
        this.Compare = Compare;
    }


    protected int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
    protected int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
    protected int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

    protected bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _elements.Count;
    protected bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _elements.Count;
    protected bool IsRoot(int elementIndex) => elementIndex == 0;

    protected T GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
    protected T GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
    protected T GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

    protected void Swap(int firstIndex, int secondIndex)
    {
        var temp = _elements[firstIndex];
        _elements[firstIndex] = _elements[secondIndex];
        _elements[secondIndex] = temp;
    }

    public bool IsEmpty()
    {
        return _elements.Count == 0;
    }

    public T Peek()
    {
        if (_elements.Count == 0)
            return default(T);

        return _elements[0];
    }

    public T Pop()
    {
        if (_elements.Count == 0)
            return default(T);

        var result = _elements[0];
        //_elements[0] = _elements[_elements.Count - 1];
        //_size--;
        _elements.RemoveAt(0);

        ReCalculateDown(Compare);

        return result;
    }

    public void Add(T element)
    {
        //if (_size == _elements.Count)
        //    throw new IndexOutOfRangeException();

        _elements.Add(element);
        //_size++;

        ReCalculateUp(Compare);
    }

    public void Remove(T element)
    {
        _elements.Remove(element);

        ReCalculateDown(Compare);
    }

    protected abstract void ReCalculateDown(Func<T, T, bool> Compare);

    protected abstract void ReCalculateUp(Func<T, T, bool> Compare);

    public void UpdateCompareMethod(Func<T, T, bool> Compare)
    {
        this.Compare = Compare;
    }

    public bool Contains(T t)
    {
        return _elements.Contains(t);
    }

    public T[] ToArray()
    {
        return _elements.ToArray();
    }
}


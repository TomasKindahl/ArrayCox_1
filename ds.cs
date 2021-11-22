using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds
{
    // Array
    public class Array<T>
    {
        private T[] arr;
        int currentSize;
        public Array(int mSize)
        {
            arr = new T[mSize];
            currentSize = 0;
        }
        public void Append(T val)
        {
            arr[currentSize] = val;
            currentSize++;
        }
        public void Append(T[] vals)
        {
            foreach (T v in vals)
                Append(v);
        }
        public T Get(int ix)
        {
            if (ix >= currentSize)
                throw new IndexOutOfRangeException("SLList.Get: index out of bounds");
            return arr[ix];
        }
        public void Set(int ix, T val)
        {
            if (ix >= currentSize)
                throw new IndexOutOfRangeException("SLList.Get: index out of bounds");
            arr[ix] = val;
        }
        public void InsertAt(int ix, T val)
        {
            for (int i = currentSize; i > ix; i--)
            {
                arr[i] = arr[i - 1];
            }
            arr[ix] = val;
            currentSize++;
        }
        public void DeleteAt(int ix)
        {
            for (int i = ix; i < currentSize - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            currentSize--;
        }
        public int Length()
        {
            return currentSize;
        }
        public void Print()
        {
            for (int i = 0; i < currentSize; i++)
            {
                Console.Write($"{i}:{arr[i]} ");
            }
            Console.WriteLine();
        }
    }
    // Singly Linked List
    public class SLList<T>
    {
        private T value;
        private SLList<T> next;
        int currentSize;
        public SLList(int maxSize) // Isn't used!
        {
            value = default(T);
            currentSize = 0;
        }
        public void Append(T val)
        {
            SLList<T> link = this;
            while (link.next != null)
                link = link.next;
            link.next = new SLList<T>(1);
            link._set(0, val);
            currentSize++;
        }
        public void Append(T[] vals)
        {
            foreach (T v in vals)
                Append(v);
        }
        public T Get(int ix)
        {
            return _get(ix);
        }
        private T _get(int ix)
        {
            SLList<T> link = this;
            for (int i = 0; i < ix && link.next != null; i++)
                link = link.next;
            return link.value;
        }
        public void Set(int ix, T val)
        {
            _set(ix, val);
        }
        private void _set(int ix, T val)
        {
            SLList<T> link = this;
            for (int i = 0; i < ix && link.next != null; i++)
                link = link.next;
            link.value = val;
        }
        public void InsertAt(int ix, T val)
        {
            SLList<T> link = this;
            for (int i = 1; i < ix && link.next != null; i++)
                link = link.next;
            SLList<T> newLink = new SLList<T>(1);
            newLink.value = val;
            newLink.next = link.next;
            link.next = newLink;
            currentSize++;
        }
        public void DeleteAt(int ix)
        {
            SLList<T> link = this;
            for (int i = 1; i < ix && link.next != null; i++)
                link = link.next;
            link.next = link.next.next;
            currentSize++;
        }
        public int Length()
        {
            return currentSize;
        }
        public void Print()
        {
            SLList<T> link = this;
            for (int i = 0; link.next != null; i++, link = link.next)
            {
                    Console.Write($"{i}:{link.value} ");
            }
            Console.WriteLine();
        }
    }
}

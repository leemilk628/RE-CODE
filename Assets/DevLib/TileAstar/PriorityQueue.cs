using System;
using System.Collections.Generic;

namespace DevLib.TileAstar
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        public List<T> Heap = new List<T>();
        public int Count => Heap.Count;
        
        public void Clear() => Heap?.Clear();

        public T Contains(T t)
        {
            int idx = Heap.IndexOf(t);
            if (idx < 0) return default(T);
            return Heap[idx];
        }

        public void Push(T data)
        {
            Heap.Add(data); //맨 끝에 데이터를 넣어주고
            HeapifyUp(Heap.Count - 1); //맨 끝 인덱스부터 올라가면서 자리를 찾는다.
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIdx = (index - 1) / 2;
                if (Heap[index].CompareTo(Heap[parentIdx]) < 0)
                    break;
                
                (Heap[index], Heap[parentIdx]) = (Heap[parentIdx], Heap[index]);
                index = parentIdx;
            }
        }

        public void DecreaseKey(T item)
        {
            int index = Heap.IndexOf(item);
            if(index < 0) return;
            
            HeapifyUp(index); //해당 인덱스부터 위로 올리면서 검사한다.
        }

        public T Pop()
        {
            T ret = Heap[0];
            
            int lastIdx = Heap.Count - 1;
            Heap[0] = Heap[lastIdx]; //마지막 원소를 앞으로 가져온다.
            Heap.RemoveAt(lastIdx); //마지막 원소는 제거
            lastIdx--;

            int now = 0;

            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now; //다음에다가 자기자신을 넣어두고
                if(left <= lastIdx && Heap[next].CompareTo(Heap[left]) < 0)
                    next = left; //만약 왼쪽이 더 작다면 다음에 갈곳은 왼쪽이다.
                if (right <= lastIdx && Heap[next].CompareTo(Heap[right]) < 0)
                    next = right;
                
                if(next == now)
                    break;
                
                (Heap[now], Heap[next]) = (Heap[next], Heap[now]);
                now = next;
            }
            
            return ret;
        }
        
        public T Peek()
        {
            return Heap.Count == 0 ? default(T) : Heap[0];    
        }
    }
    
}
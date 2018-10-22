using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Sorting
{
    public class MinPQ<T>
    {
        private readonly List<IComparable<T>> _pq;
        private int N { get; set; } = 0;

        public MinPQ(int maxN)
        {
            _pq = new List<IComparable<T>>(new IComparable<T>[maxN + 1]);
        }

        public bool IsEmpty { get => N == 0; }
        public int Size { get => N; }
        public void Insert(IComparable<T> v)
        {
            _pq[++N] = v;
            Swim(N);
        }

        public IComparable<T> DeleteMin()
        {
            var max = _pq[1];
            Exchange(1, N--);
            _pq[N + 1] = null;
            Sink(1);
            return max;
        }

        private bool Less(int i, int j)
        {
            return _pq[i].CompareTo((T)_pq[j]) < 0;
        }

        private void Exchange(int i, int j)
        {
            var t = _pq[i];
            _pq[i] = _pq[j];
            _pq[j] = t;
        }

        /// <summary>
        /// Bottom-up reheapify (swim).
        /// </summary>
        /// <param name="k"></param>
        private void Swim(int k)
        {
            while(k > 1 && Less(k/2, k))
            {
                Exchange(k / 2, k);
                k = k / 2;
            }
        }

        /// <summary>
        /// Top-down reheapify (sink).
        /// </summary>
        /// <param name="k"></param>
        private void Sink(int k)
        {
            while(2*k <= N)
            {
                int j = 2 * k;
                if (j < N && Less(j, j + 1)) j++;
                if (!Less(k, j)) break;
                Exchange(k, j);
                k = j;
            }
        }

    }
}

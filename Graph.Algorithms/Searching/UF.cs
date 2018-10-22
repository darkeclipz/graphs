using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Searching
{
    public class UF
    {
        private readonly List<int> _id;

        public UF(int N)
        {
            Count = N;
            _id = new List<int>(new int[N]);
            for (int i = 0; i < N; i++)
                _id[i] = i;
        }

        public int Count { get; private set; }

        public bool Connected(int p, int q) => Find(p) == Find(q);
        public int Find(int p) => _id[p];
        public void Union(int p, int q)
        {
            int pID = Find(p);
            int qID = Find(q);
            if (pID == qID) return;
            for (int i = 0; i < _id.Count; i++)
                if (_id[i] == pID) _id[i] = qID;
            Count--;
        }

    }
}

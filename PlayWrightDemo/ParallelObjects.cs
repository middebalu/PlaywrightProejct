using System;
using System.Collections.Generic;
using System.Text;

namespace PlayWrightDemo
{
    internal class ParallelObjects
    {
        private ThreadLocal<int> count;
        public void InitCount()
        {
            count = new ThreadLocal<int>();
            count.Value = 1;
        }

        public ThreadLocal<int> getCount()
        {
            return count;
        }

        public void setCount(int temp)
        {
            this.count.Value = temp;
        }

        public void DisposeCount()
        {
            count.Dispose();
        }
    }
}

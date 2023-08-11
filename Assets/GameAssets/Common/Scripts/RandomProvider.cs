using System;
using System.Threading;

namespace Common
{
    public static class RandomProvider
    {
        private static int m_Seed = Environment.TickCount;
        private static ThreadLocal<Random> m_RandomWrapper = new(() => new Random(Interlocked.Increment(ref m_Seed)));

        public static Random GetThreadRandom()
        {
            return m_RandomWrapper.Value;
        }
    }
}
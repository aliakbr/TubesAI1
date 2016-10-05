using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes1AI.Scheduling
{
    class myRandom
    {
        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(List<int> domainHari)
        {
            lock (syncLock)
            { // synchronize
                while (true)
                {
                    int random = getrandom.Next(0, 24);
                    foreach (int hari in domainHari)
                    {
                        if (random == hari)
                        {
                            return hari;
                        }
                    }
                }
            }
        }
        //Function to get random number
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max+1);
            }
        }

        public static double GetRandomFloat(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

    }
}

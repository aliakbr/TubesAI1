using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    File Name: myRandom.cs
    Description: Kelas untuk generate random integer / float.
*/

namespace TubesAI1.Scheduling
{
    class myRandom
    {
        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        /*
            @param arrayInt : List of integer
            @return : a random member of arrayInt
        */
        public static int GetRandomNumber(List<int> arrayInt)
        {
            lock (syncLock)
            { // synchronize
                while (true)
                {
                    int random = getrandom.Next(0, 24);
                    foreach (int hari in arrayInt)
                    {
                        if (random == hari)
                        {
                            return hari;
                        }
                    }
                }
            }
        }

        /*
            @param min : mininum value
            @param max : maximum value
            @return : a random integer between min and max
        */
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        /*
            @param minimum : mininum value
            @param maximum : maximum value
            @return : a random float between min and max
        */
        public static double GetRandomFloat(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes1AI.Scheduling
{
    class Individu : IComparable<Individu>
    {
        private int id;
        private KelasManagement kelasManagement; //DNA
        private int fitness;
        
        public Individu()
        {
            kelasManagement = new KelasManagement();
            fitness = 9999; //random fitness
        }
        public Individu(KelasManagement k, int m, int id)
        {
            this.id = id;
            List<Kelas> T = new List<Kelas>(k.getArrayKelas().Count);
            foreach (Kelas k1 in k.getArrayKelas())
            {
                Kelas knew = new Kelas(k1);
                T.Add(knew);
            }
            kelasManagement = new KelasManagement(T);
            fitness = m;
        }


        public Individu(Individu i)
        {
            kelasManagement = i.getDNA();
            fitness = i.getFitness();
        }

        public void generateDNA()
        {
            for (int i = 0; i < kelasManagement.getArrayKelas().Count(); i++)
            {
                kelasManagement.setRandomValue(i);
            }
            fitness = kelasManagement.getConflict();
        }

        public KelasManagement getDNA()
        {
            return kelasManagement;
        }

        public int getFitness()
        {
            return fitness;
        }

        public int CompareTo(Individu i)
        {
            if (i == null)
            {
                return 1;
            }
            else
            {
                return this.fitness.CompareTo(i.getFitness());
            }
        }

        public void print()
        {
            Console.WriteLine("ID Individu :" + id);
            Console.WriteLine("total conflict :" + fitness);
            Console.WriteLine("---------------------------");
        }
    }
}

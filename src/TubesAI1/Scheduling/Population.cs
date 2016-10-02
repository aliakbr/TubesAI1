using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes1AI.Scheduling
{
    class Population
    {
        private List<Individu> organisms;

        public Population()
        {
            organisms = new List<Individu>();
        }

        public Population(Population P)
        {
            organisms = P.organisms;
        }

        public void generatePopulation(int n, KelasManagement m)
        //generate n populasi
        {
            organisms = new List<Individu>(n);
            Individu baru;
            for (int i = 0; i < n; i++)
            {
                baru = new Individu(m, m.getConflict(), i);
                organisms.Add(baru);
                organisms[i].generateDNA();
            }

            //Display tiap individu
            foreach (Individu o in organisms)
            {
                o.print();
            }
            //Sorting list organisme sesuai dengan fitnessnya.
            organisms.Sort();
            foreach (Individu o in organisms)
            {
                o.print();
            }
        }

        public List<Individu> getPopulation()
        {
            return organisms;
        }

        public Individu getIndividu(int i)
        {
            return organisms[i];
        }

        public void sortPopulasi()
        {
            organisms.Sort();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    File Name: SimulatedAnnealing.cs
    Description: Class untuk menjalankan Simulated annealing
*/
namespace Tubes1AI.Scheduling
{
    class SimulatedAnnealing
    {
        private RuanganManagement ruanganManagement;
        private KelasManagement kelasManagement;
        private KelasManagement solution;

        /*
            Constructor
        */
        public SimulatedAnnealing(KelasManagement kelasManagement, RuanganManagement ruanganManagement)
        {
            this.kelasManagement = kelasManagement;
            this.ruanganManagement = ruanganManagement;
        }

        /*
            @param temperature : temperatur awal
            @param cooling_rate : cooling rate temperatur
            Menjalankan simulated annealing dan menuliskan jumlah konflik yang terjadi
        */
        public void execute(float temperature, float cooling_rate)
        {
            this.solution = new KelasManagement(this.kelasManagement.getArrayKelas());
            Console.WriteLine("Initial conflict: "+this.solution.getConflict());
            solution.printall();
            Console.WriteLine();
            Console.WriteLine("Temperature: " + temperature);
            Console.WriteLine("Cooling rate: " + cooling_rate);
            Console.WriteLine("---------");


            while (temperature > 1)
            {
                for(int i=0; i<200; i++)
                {
                    int random = myRandom.GetRandomNumber(0, this.kelasManagement.getArrayKelas().Count - 1);
                    this.kelasManagement.setRandomValue(random);
                    int newConflict = this.kelasManagement.getConflict();
                    int currentConflict = this.solution.getConflict();
                    if (newConflict < currentConflict)
                    {
                        this.solution = new KelasManagement(this.kelasManagement.getArrayKelas());
                        Console.WriteLine("Found better: " + newConflict);
                    }
                    else
                    {
                        if (myRandom.GetRandomFloat(0,100) <= Math.Exp((currentConflict - newConflict) / temperature))
                        {
                            this.solution = new KelasManagement(this.kelasManagement.getArrayKelas());
                            Console.WriteLine("Assign worse: " + newConflict);
                        }
                    }
                }
                temperature *= cooling_rate;
            }

            //solution.printall();
            this.solution.printall();
            Console.WriteLine("---------");
            Console.WriteLine("Last conflict: " + this.solution.getConflict());
        }

        public KelasManagement getSol()
        {
            return solution;
        }

    }
}

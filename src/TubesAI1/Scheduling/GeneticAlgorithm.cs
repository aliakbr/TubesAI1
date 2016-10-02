using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes1AI.Scheduling
{
    class GeneticAlgorithm
    {
        private Population P;

        public GeneticAlgorithm(Population P)
        {
            this.P = P;
        }

        public void crossover()
        //crossover 2 individu paling atas;
        {
            int chromosomeLength = P.getIndividu(0).getDNA().getArrayKelas().Count - 1;
            //Ambil random point
            int randomPoint = myRandom.GetRandomNumber(0, chromosomeLength);

            //2 point crossover di point tersebut.
            int randomCrossOver = myRandom.GetRandomNumber(0, 2);
            int temp;
            switch (randomCrossOver)
            {
                case 0:
                    temp = P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getHari();
                    if (P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getHari() != P.getIndividu(1).getDNA().getArrayKelas()[randomPoint].getHari())
                    {
                        P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].setHari(P.getIndividu(1).getDNA().getArrayKelas()[randomPoint].getHari());
                        P.getIndividu(1).getDNA().getArrayKelas()[randomPoint].setHari(temp);
                    }
                    break;
                case 1:
                    temp = P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getMulai();
                    P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].setMulai(P.getIndividu(1).getDNA().getArrayKelas()[randomPoint].getMulai());
                    P.getIndividu(1).getDNA().getArrayKelas()[randomPoint].setMulai(temp);
                    break;
                case 2:
                    Ruangan tempR = new Ruangan(P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getRuangan());
                    P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].setRuangan(P.getIndividu(1).getDNA().getArrayKelas()[randomPoint].getRuangan());
                    P.getIndividu(1).getDNA().getArrayKelas()[randomPoint].setRuangan(tempR);
                    break;
                default :
                    break;
            }
            //print konflik hasil crossover
            Console.WriteLine("Hasil Crossover :");
            P.getIndividu(0).print();
            P.getIndividu(1).print();
            Console.WriteLine("--------------------");
        }

        public void mutation()
        {
            int chromosomeLength = P.getIndividu(0).getDNA().getArrayKelas().Count - 1;

            //Ambil random point
            int randomPoint = myRandom.GetRandomNumber(0, chromosomeLength);

            // ambil random Mutation Point
            int randomMutation = myRandom.GetRandomNumber(0, 2);
            int temp;
            switch (randomMutation)
            {
                case 0:
                    temp = P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getHari();
                    while (P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getHari() == temp)
                    {
                        P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].setRandomValue();
                    }
                    break;
                case 1:
                    temp = P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getMulai();
                    while (P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getMulai() == temp)
                    {
                        P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].setRandomValue();
                    }
                    break;
                case 2:
                    Ruangan tempR = new Ruangan(P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getRuangan());
                    while (P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].getRuangan() == tempR)
                    {
                        P.getIndividu(0).getDNA().getArrayKelas()[randomPoint].setRandomValue();
                    }
                    break;
                default:
                    break;
            }
            //print hasil mutasi
            Console.WriteLine("hasil mutasi :");
            P.getIndividu(0).print();
            Console.WriteLine("--------------");
        }

        public KelasManagement getSolution()
        {
            P.sortPopulasi();
            return P.getIndividu(0).getDNA();
        }
    }
}

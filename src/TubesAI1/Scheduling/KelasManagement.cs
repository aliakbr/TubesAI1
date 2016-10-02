using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GeneticAlgorithmTesting.StrukturData;

namespace Tubes1AI.Scheduling
{
    class KelasManagement
    {
        private List<Kelas> arrayKelas;

        public KelasManagement()
        {
            this.arrayKelas = new List<Kelas>();
        }

        public KelasManagement(List<Kelas> arrayKelas)
        {
            this.arrayKelas = new List<Kelas>();
            foreach(Kelas k in arrayKelas)
            {
                Kelas k2 = new Kelas(k);
                this.addKelas(k2);
            }
        }

        public void addKelas(Kelas kelas)
        {
            this.arrayKelas.Add(kelas);
        }

        public int getConflict()
        {
            int count = 0;
            for(int i=0; i < this.arrayKelas.Count-1; i++)
            {
                for(int j=i+1; j < this.arrayKelas.Count; j++)
                {
                    if(isConflict(this.arrayKelas[i], this.arrayKelas[j]))
                    {
                        //Console.WriteLine("Conflict: " + this.arrayKelas[i].getNama() + " dan " + this.arrayKelas[j].getNama());
                        count++;
                    }
                }
            }
            return count;
        }

        private bool isConflict(Kelas k1, Kelas k2)
        {
            if(k1.getNamaRuangan() != k2.getNamaRuangan())
            {
                return false;
            }
            if (k1.getHari() != k2.getHari())
            {
                return false;
            }

            if (k1.getMulai() < k2.getMulai())
            {
                if(k1.getMulai()+k1.getDurasi() <= k2.getMulai())
                {
                    return false;
                }
            }
            else if (k1.getMulai() > k2.getMulai())
            {
                if (k2.getMulai() + k2.getDurasi() <= k1.getMulai())
                {
                    return false;
                }
            }
            return true;
        }

        public List<Kelas> getArrayKelas()
        {
            return this.arrayKelas;
        }

        public void setRandomValue(int i)
        {
            do
            {
                arrayKelas[i].setRandomValue();
                //Console.WriteLine("Try");
            } while (!arrayKelas[i].isCurrentValid());
        }

        public void printall()
         {
             //print all
             int count = this.arrayKelas.Count;
             for (int i = 0; i<count; i++)
             {
                 this.arrayKelas[i].print();
             }
         }

        public bool IsAdaConflict(Kelas k)
        {
                        /* fungsi ini mengmbalikan nilai true jika kelas k memiliki jadwal yang bertabrakan/kkonflik dengan min salah satu dari semua matkul */
                        for (int i = 0; i < this.arrayKelas.Count; i++)
            {
                                if (k.getNama().CompareTo(this.arrayKelas[i].getNama()) != 0)
                {
                                        if (isConflict(k, this.arrayKelas[i]))
                    {
                                                return true;
                                            }
                                    }
                            }
                        return false;
                    }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    File Name: KelasManagement.cs
    Description: Berfungsi untuk mengelola Kelas2
*/
namespace TubesAI1.Scheduling
{
    class KelasManagement
    {
        // Array of Kelas
        private List<Kelas> arrayKelas;

        /*
            Constructor
        */
        public KelasManagement()
        {
            this.arrayKelas = new List<Kelas>();
        }

        /*
            Semacam Copy Constructor
        */
        public KelasManagement(List<Kelas> arrayKelas)
        {
            this.arrayKelas = new List<Kelas>();
            foreach(Kelas k in arrayKelas)
            {
                Kelas k2 = new Kelas(k);
                this.addKelas(k2);
            }
        }

        /*
            @param Kelas : kelas
            Menambahkan Kelas ke dalam arrayKelas
        */
        public void addKelas(Kelas kelas)
        {
            this.arrayKelas.Add(kelas);
        }


        /*
            @return jumlah Kelas yang jadwalnya bertabrakan (conflict)
            
            Catatan: Kayaknya masih salah cara ngitungnya
        */  
        public int getConflict()
        {
            int count = 0;
            for(int i=0; i < this.arrayKelas.Count-1; i++)
            {
                for(int j=i+1; j < this.arrayKelas.Count; j++)
                {
                    if(isConflict(this.arrayKelas[i], this.arrayKelas[j]))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /*
            @param k1 : Kelas 1
            @param k2 : Kelas 2
            @return tru jika jadwal Kelas 1 dan Kelas 2 bertabrakan
        */
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

        /*
            @param i : index
            Assign random value untuk Kelas[i]
        */
        public void setRandomValue(int i)
        {
            arrayKelas[i].setRandomValue();
        }

        /**********************
                GETTER
        **********************/
        public List<Kelas> getArrayKelas()
        {
            return this.arrayKelas;
        }
    }
}

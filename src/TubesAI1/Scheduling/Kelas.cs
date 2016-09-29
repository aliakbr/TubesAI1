using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    File Name: Kelas.cs
    Description: Class ini adalah variabel pada permasalahan ini.
*/

namespace TubesAI1.Scheduling
{
    class Kelas
    {
        // Value yang dimiliki 'Kelas' saat ini
        // 3 variable inilah yang diassign value
        private Ruangan currentRuangan;         
        private int currentMulai;               // Contoh: 7 (mulai kelas jam 7)
        private int currentHari;                // Contoh: 1 (Senin)

        // Domain value yang dimiliki 'Kelas'
        private List<Ruangan> domainRuangan;    
        private List<int> domainMulai;          // Contoh: [7, 8, 9] (Kelas hanya bisa mulai jam 7, 8, atau 9)
        private List<int> domainHari;           // Contoh: [1, 3, 5] (Kelas hanya bisa dilakukan hari Senin, Rabu, atau Jumat)

        // Nama dan durasi 'Kelas'
        private string nama;                    // Contoh: "IF2110"
        private int durasi;                     // Contoh: 4 (4 jam)

        private RuanganManagement ruanganManagement;
        
        /*
            Constructor Kelas
        */
        public Kelas(string nama, string ruangan, int mulai, int selesai, int durasi, List<int> hari, RuanganManagement ruanganManagement)
        {
            this.ruanganManagement = ruanganManagement;
            if (ruangan == "-") // Jika tidak ada constraint ruangan
            {
                this.domainRuangan = ruanganManagement.getAllRuangan();
            }
            else
            {
                this.currentRuangan = ruanganManagement.getRuangan(ruangan);
                this.domainRuangan = new List<Ruangan>();
                this.domainRuangan.Add(ruanganManagement.getRuangan(ruangan));
            }
                       
            // Assign value dan domain 'Kelas'
            this.nama = nama;
            this.durasi = durasi;
            this.domainHari = hari;
            this.domainMulai = new List<int>();
            for(int i= mulai; i <= selesai-durasi; i++)
            {
                this.domainMulai.Add(i);
            }

            // Assign random value sampai value yang dimiliki valid (tidak melanggar constraint ruangan)
            do
            {
                this.setRandomValue();
            } while (!this.isCurrentValid());
        }

        /*
           Copy Constructor Kelas
       */
        public Kelas(Kelas kelas)
        {
            this.ruanganManagement = kelas.ruanganManagement;
            this.nama = kelas.nama;
            this.durasi = kelas.durasi;
            this.currentRuangan = kelas.currentRuangan;
            this.currentMulai = kelas.currentMulai;
            this.currentHari = kelas.currentHari;
            this.domainRuangan = kelas.domainRuangan;
            this.domainMulai = kelas.domainMulai;
            this.domainHari = kelas.domainHari;
        }

        /*
            Assign random value 
        */
        public void setRandomValue()
        {
            if (domainRuangan.Count > 1)
            {
                this.currentRuangan = this.ruanganManagement.getRandomRuangan();
            }
            this.currentMulai = myRandom.GetRandomNumber(this.domainMulai);
            this.currentHari = myRandom.GetRandomNumber(this.domainHari);
        }

        /*
            Print semua data member 'Kelas' (untuk debugging)
        */
        public void print()
        {
            Console.WriteLine("------------");
            Console.WriteLine("Kelas: "+this.nama);
            Console.Write("Tempat: " + this.currentRuangan.getName()+"  ( ");
            foreach (Ruangan ruangan in this.domainRuangan)
            {
                Console.Write(ruangan.getName() + " ");
            }
            Console.WriteLine(")");
            Console.Write("Hari: " + this.currentHari + "  ( ");
            foreach (int h in this.domainHari)
            {
                Console.Write(h + " ");
            }
            Console.WriteLine(")");
            Console.WriteLine("Waktu: " + this.currentMulai + "-" + (this.currentMulai + this.durasi));
            Console.WriteLine();
            Console.WriteLine(this.isCurrentValid());
            Console.WriteLine("------------");
        }
        
        /*
            @return true jika value saat ini valid (tidak melanggar constraint Ruangan)
        */
        public bool isCurrentValid()
        {
            return currentRuangan.isOpen(this.currentMulai, this.currentMulai + this.durasi, this.currentHari);
        }


        /**********************
                GETTER
        **********************/
        public string getNamaRuangan()
        {
            return this.currentRuangan.getName();
        }

        public int getHari()
        {
            return this.currentHari;
        }

        public int getMulai()
        {
            return this.currentMulai;
        }

        public int getDurasi()
        {
            return this.durasi;
        }

        public string getNama()
        {
            return this.nama;
        }
    }
}
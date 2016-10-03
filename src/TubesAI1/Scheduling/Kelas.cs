using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes1AI.Scheduling
{
    class Kelas
    {
        private Ruangan currentRuangan;
        private int currentMulai;
        private int currentHari;

        private string nama;
        private int durasi;

        private List<Ruangan> domainRuangan;
        private List<int> domainMulai;
        private List<int> domainHari;

        private RuanganManagement ruanganManagement;

        
        /////////// Contoh cara make:
        //
        // List<int> hari_k1 = new List<int>();
        // hari_k1.Add(1);
        // hari_k1.Add(2);
        // hari_k1.Add(3);
        // hari_k1.Add(4);
        // hari_k1.Add(5);
        // List<string> constrain_ruangan= new List<string>();
        // constrain_ruangan.Add("7602");
        // constrain_ruangan.Add("7610");
        // Kelas k1 = new Kelas("IF2110", constrain_ruangan, 7, 12, 4, hari_k1, rm);
        public Kelas(string nama, List<string> ruangan, int mulai, int selesai, int durasi, List<int> hari, RuanganManagement ruanganManagement)
        {
            this.ruanganManagement = ruanganManagement;
            if (ruangan[0] == "-")
            {
                this.domainRuangan = ruanganManagement.getAllRuangan();
            }
            else
            {
                this.currentRuangan = ruanganManagement.getRuangan(ruangan[0]);
                this.domainRuangan = new List<Ruangan>();
                foreach(string r in ruangan) {
                    this.domainRuangan.Add(ruanganManagement.getRuangan(r));
                }
            }
                       
            this.nama = nama;
            this.durasi = durasi;

            this.domainHari = hari;
            this.domainMulai = new List<int>();
            for(int i= mulai; i <= selesai-durasi; i++)
            {
                this.domainMulai.Add(i);
            }

            
                this.setRandomValue();
            
            
            
        }

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

        public void setRandomValue()
        {
            do
            {
                if (domainRuangan.Count > 1)
                {
                    this.currentRuangan = this.ruanganManagement.getRandomRuangan();
                }
                this.currentMulai = myRandom.GetRandomNumber(this.domainMulai);
                this.currentHari = myRandom.GetRandomNumber(this.domainHari);
            } while (!this.isCurrentValid());
            
        }

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
        
        public bool isCurrentValid()
        {
            return currentRuangan.isOpen(this.currentMulai, this.currentMulai + this.durasi, this.currentHari);
        }

        public List<Ruangan> getDomainRuangan()
        {
            return this.domainRuangan;
        }
 
        public List<int> getDomainMulai()
        {
            return this.domainMulai;
        }
 
        public List<int> getDomainHari()
        {
           return this.domainHari;
        }

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

        public void setMulai(int i)
        {
            currentMulai = i;
        }

        public void setHari(int i)
        {
            currentHari = i;
        }
        
        public void setRuangan(Ruangan R)
        {
            currentRuangan = R;
        }

        public Ruangan getRuangan()
        {
            return currentRuangan;
        }
    }
}
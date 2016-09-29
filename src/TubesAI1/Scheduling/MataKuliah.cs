using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubesAI1.Scheduling
{
    public class MataKuliah
    {
        private string nama;
        private string hari;
        private int awal;
        private int akhir;
        private int durasi;
        private string ruangan;

        public MataKuliah(string nama = "", string hari = "", int awal = 0, int selesai = 0, int durasi = 0, string ruangan = "")
        {
            this.nama = nama;
            this.hari = hari;
            this.awal = awal;
            this.akhir = selesai;
            this.durasi = durasi;
            this.ruangan = ruangan;
        }

        //Getter
        public string getNama()
        {
            return nama;
        }

        public string getHari()
        {
            return hari;
        }

        public int getAwal()
        {
            return awal;
        }

        public int getAkhir()
        {
            return akhir;
        }

        public int getDurasi()
        {
            return durasi;
        }

        public string getRuangan()
        {
            return ruangan;
        }

        //Settter
        public void setNama(string name)
        {
            nama = name;
        }

        public void setRuangan(string r)
        {
            ruangan = r;
        }

        public void setAwal(int a)
        {
            awal = a;
        }

        public void setAkhir(int a)
        {
            akhir = a;
        }

        public void setDurasi(int d)
        {
            durasi = d;
        }

        public void setHari(string d)
        {
            hari = d;
        }
    }
}
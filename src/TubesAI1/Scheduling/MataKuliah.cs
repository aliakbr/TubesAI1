using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes1AI.Scheduling
{
    public class MataKuliah
    {
        private string nama;
        private string hari;
        private int awal;
        private int akhir;
        private int durasi;
        private string ruangan;

        public MataKuliah()
        {
            nama = "";
            hari = "";
            awal = 0;
            akhir = 0;
            durasi = 0;
            ruangan = "";
        }
        public MataKuliah(string nama1, string hari1, int awal1, int selesai1, int durasi1, string ruangan1)
        {
            nama = nama1;
            hari = hari1;
            awal = awal1;
            akhir = selesai1;
            durasi = durasi1;
            ruangan = ruangan1;
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

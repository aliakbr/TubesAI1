using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    File Name: Ruangan.cs
    Description: Class Ruangan
*/
namespace TubesAI1.Scheduling
{
    class Ruangan
    {
        private string nama;                // Contoh: "7602"
        private int jam_buka;               // Contoh: 7 (buka mulai jam 7)
        private int jam_tutup;              // Contoh: 14 (tutup jam 14)
        private List<int> hari_buka;        // Contoh: [1,2] (buka hari Senin dan Selasa)

        /*
            Constructor
        */
        public Ruangan(string nama, int jam_buka, int jam_tutup, List<int> hari)
        {
            this.nama = nama;
            this.jam_buka = jam_buka;
            this.jam_tutup = jam_tutup;
            this.hari_buka = hari;
        }

        /*
            @param mulai : jam mulai
            @param selesai : jam selesai
            @param hari : hari
            @return true jika kelas BUKA pada hari 'hari' pada jam 'mulai' s/d jam 'selesai' 
        */
        public bool isOpen(int mulai, int selesai, int hari)
        {
            if (mulai < this.jam_buka || selesai > this.jam_tutup || !this.isHariOpen(hari))
            {
                return false;
            }
            return true;
        }

        /*
            @param hari : hari
            @return true jika kelas BUKA pada hari 'hari'
        */
        private bool isHariOpen(int hari)
        {
            bool open = false;
            foreach (int h in this.hari_buka)
            {
                if (hari == h)
                {
                    open = true;
                }
            }
            return open;
        }

        /**********************
                GETTER
        **********************/
        public string getName()
        {
            return this.nama;
        }
    }
}

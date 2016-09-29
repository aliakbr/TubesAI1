using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    File Name: RuanganManagement.cs
    Description: Berfungsi untuk mengelola Ruangan2
*/
namespace TubesAI1.Scheduling
{
    class RuanganManagement
    {
        // Array of Ruangan
        private List<Ruangan> arrayRuangan;

        /*
            Constructor
        */
        public RuanganManagement()
        {
            this.arrayRuangan = new List<Ruangan>();
        }

        /*
            @param Ruangan : ruangan
            Menambahkan ruangan ke dalam arrayRuangan
        */
        public void addRuangan(Ruangan ruangan)
        {
            this.arrayRuangan.Add(ruangan);
        }

        /*
            @param nama : nama ruangan
            @param mulai : jam mulai
            @param selesai : jam selesai
            @param hari : hari
            @return true jika ruangan dengan nama 'nama' buka pada hari 'hari' pada jam 'mulai' s/d jam 'selesai'
        */
        public bool isRuanganOpen(string nama, int mulai, int selesai, int hari)
        {
            bool open = false;
            foreach(Ruangan r in this.arrayRuangan)
            {
                if(r.getName() == nama)
                {
                    open = r.isOpen(mulai, selesai, hari);
                }
            }
            return open;
        }


        /**********************
                GETTER
        **********************/
        public Ruangan getRandomRuangan()
        {
            return this.arrayRuangan[myRandom.GetRandomNumber(0, this.arrayRuangan.Count - 1)];
        }

        public Ruangan getRuangan(string nama)
        {
            foreach(Ruangan r in this.arrayRuangan)
            {
                if(r.getName() == nama)
                {
                    return r;
                }
            }
            return null;
        }       

        public List<Ruangan> getAllRuangan()
        {
            return arrayRuangan;
        }
    }
}

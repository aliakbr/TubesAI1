using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes1AI.Scheduling
{
    class RuanganManagement
    {
        private List<Ruangan> ruangans;
        public RuanganManagement()
        {
            this.ruangans = new List<Ruangan>();
        }

        public void addRuangan(Ruangan ruangan)
        {
            this.ruangans.Add(ruangan);
        }

        public bool isRuanganOpen(string nama, int mulai, int selesai, int hari)
        {
            bool open = false;
            foreach(Ruangan r in this.ruangans)
            {
                if(r.getName() == nama)
                {
                    open = r.isOpen(mulai, selesai, hari);
                }
            }
            return open;
        }

        public Ruangan getRuangan(string nama)
        {
            foreach(Ruangan r in this.ruangans)
            {
                if(r.getName() == nama)
                {
                    return r;
                }
            }
            return null;
        }

        public Ruangan getRandomRuangan()
        {
            return this.ruangans[myRandom.GetRandomNumber(0, this.ruangans.Count-1)];
        }

        public List<Ruangan> getAllRuangan()
        {
            return ruangans;
        }

        public Double getEfektifitas()
        {
            int WaktuTerpakai = 0;
            int WaktuAvailable = 0;
            foreach (Ruangan R in ruangans)
            {
                WaktuTerpakai += R.getWaktuTerpakai();
                WaktuAvailable += R.getWaktuAvailable();
            }

            return (Convert.ToDouble(WaktuTerpakai) / Convert.ToDouble(WaktuAvailable)) * 100;
        }
    }
}

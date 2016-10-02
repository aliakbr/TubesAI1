namespace Tubes1AI.Scheduling
{
    class HillClimbing
    {
        /* atribut */
        private RuanganManagement ruangan_management;
        private KelasManagement kelas_management, solution;

        /* ctor */
        public HillClimbing(KelasManagement kelas_management, RuanganManagement ruangan_management) {
            this.ruangan_management = ruangan_management;
            this.kelas_management = kelas_management;
            solution = new KelasManagement(kelas_management.getArrayKelas());
        }

        public KelasManagement getSol()
        {
            return solution;
        }
        
        public void getSolution() {
            
            System.Console.WriteLine("Initial conflict: " + this.solution.getConflict());

            this.solution.printall();
            System.Console.WriteLine();
            int heuristic = this.solution.getConflict();
            int value_after = heuristic;
            int value_before = 999999; //inisiasi awal

            //iterator current matakuliah
            int iterator_mata_kuliah = 0;
            System.Console.WriteLine();
            int kemungkinan = 0; //variabel ini untuk mengantisipasi lokal maksimum
            while (this.solution.getConflict() > 0 && kemungkinan  < kelas_management.getArrayKelas().Count-1) { //tidak ada konflik dan ketemu solusi lokal
                value_before = value_after; //dapat diganti pengecekan di tengah loop

                Kelas k = new Kelas(solution.getArrayKelas()[iterator_mata_kuliah]);
                while (!solution.IsAdaConflict(k)){ //gak mungkin infinite loop karena udah di cek diatas
                    kemungkinan++;
                    iterator_mata_kuliah++;
                    if (iterator_mata_kuliah > solution.getArrayKelas().Count-1) {
                        iterator_mata_kuliah = 0;
                    }
                    k = solution.getArrayKelas()[iterator_mata_kuliah];
                }

                //dapat mata kuliah yang bisa digeser2
                
                KelasManagement dummmy_solution = new KelasManagement(solution.getArrayKelas());
                Kelas k1 = dummmy_solution.getArrayKelas()[iterator_mata_kuliah];
                
                //coba semua kemungkinan untuk current kelas
                bool isAdaLebihKecil = false;
                foreach (Ruangan r in k1.getDomainRuangan()) {
                    foreach (int m in k1.getDomainMulai()) {
                        foreach (int h in k1.getDomainHari()) {
                            k1.setHari(h);
                            k1.setMulai(m);
                            k1.setRuangan(r);
                            if (k1.isCurrentValid())
                            {
                                dummmy_solution.getArrayKelas()[iterator_mata_kuliah] = k1;
                                int value_heuristic = dummmy_solution.getConflict();
                                if (value_heuristic < value_before) {
                                    isAdaLebihKecil = true;
                                    value_before = value_heuristic;
                                    KelasManagement sol = new KelasManagement(dummmy_solution.getArrayKelas());
                                    solution = sol; 
                                }
                            }
                        }
                    }
                }
                if (!isAdaLebihKecil)
                {
                    kemungkinan++;
                }
                else kemungkinan = 0;


                //cek kalo current mata kuliah tidak ada yang bentrok ke next mata kuliah (iterasi biasa atau random gak mungkin satuputaran)


                // geser semua kemungkinan dari setiap matakuliah (kalo ini udah ada yang lebih kecil konfilknya tidak usah cek mata kuliah lain)
                // dicount kalo udah satu putaran breaks


                // dapet yang lebih kecil simpan ke solution
                // jika tidak dapat lebih kecil maka solusi lokal telah ditemukan
                //break;                
            }
            //printall();
            //System.Console.WriteLine("Solusi");
            //solution.printall();
            //System.Console.WriteLine();
            this.solution.printall();
            System.Console.WriteLine("Last Conflict : {0}", solution.getConflict());
        }
    }
}
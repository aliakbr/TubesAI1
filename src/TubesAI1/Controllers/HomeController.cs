using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using Tubes1AI.Scheduling;

namespace TubesAI1.Controllers
{
    public class HomeController : Controller
    {
        private UserInput user_input = new UserInput();
        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public string gethari( int i)
        {
            string a = "";
            switch (i)
            {
                case 2:
                    a = "senin";
                    break;
                case 3:
                    a = "selasa";
                    break;
                case 4:
                    a = "rabu";
                    break;
                case 5:
                    a = "kamis";
                    break;
                case 6:
                    a = "jumat";
                    break;
                default:
                    break;
            }
            return a;
        }

        public IActionResult Index()
        {
            ViewData["Start"] = 0;
            ViewData["Length"] = 0;
            return View();
        }

        public IActionResult Error()
        {
            ViewData["Message"] = "An error has occured.";
            return View();
        }
        
        private int timeToInt (string s)
        {
            // Asumsi hanya ada 5 karakter pada s
            var sr = new StringReader(s);
            int temp;
            string buf = "";
            char b;
            b = (char)sr.Read();
            buf += b;
            b = (char)sr.Read();
            buf += b;
            temp = Int32.Parse(buf);

            return temp;
        }

        private bool cekRuanganSama (string namaRuangan, string ruangan)
        {
            var sr = new StringReader(ruangan);
            string temp = "";
            char b;
            while (sr.Peek() > -1)
            {
                temp = "";
                do
                {
                    b = (char)sr.Read(); 
                    if (b != ' ')
                    {
                        temp += b;
                    }
                } while ((b != ' ') && (sr.Peek() > -1));
                if (namaRuangan.Equals(temp))
                {
                    return true;
                }
            }
            return false;
        }


        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files, UserInput u)
        {
            user_input = u;
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    uploads = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(uploads, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            var openFile = new FileStream(uploads, FileMode.Open, FileAccess.Read);
            using (var readStream = new StreamReader(openFile))
            {
                int conflict = 0;
                string line;

                Boolean isRuangan = false;
                Boolean isKelas = false;
                string temp = "";
                char b;
                RuanganManagement listOfRuangan = new RuanganManagement();
                KelasManagement listOfKelas = new KelasManagement();
                
                
                /* ADT RUANGAN */
                string nama = "";                // Contoh: "7602"
                int jam_buka = 0;               // Contoh: 7 (buka mulai jam 7)
                int jam_tutup = 0;              // Contoh: 14 (tutup jam 14)
                List<int> hari_buka = new List<int>();

                /* ADT KELAS */
                // Domain value yang dimiliki 'Kelas'
                string ruangan = "";
                List<Ruangan> domainRuangan = new List<Ruangan>();
                List<int> domainMulai = new List<int>();          // Contoh: [7, 8, 9] (Kelas hanya bisa mulai jam 7, 8, atau 9)
                List<int> domainHari = new List<int>();           // Contoh: [1, 3, 5] (Kelas hanya bisa dilakukan hari Senin, Rabu, atau Jumat)
                int durasi = 0;                     // Contoh: 4 (4 jam)


                while ((line = readStream.ReadLine()) != null)
                {
                    if (line.Equals(""))
                    {
                        // Do nothing
                    }
                    else if (line.Equals("Ruangan"))
                    {
                        isRuangan = true;
                        isKelas = false;
                    }
                    else if (line.Equals("Jadwal"))
                    {
                        isRuangan = false;
                        isKelas = true;
                    }
                    else if (isRuangan)
                    {
                        var sr = new StringReader(line);
                        int point = 0;
                        temp = "";
                        hari_buka = new List<int>();
                        domainMulai = new List<int>();
                        domainRuangan = new List<Ruangan>();
                        for (int k = 0; k < line.Length; k++)
                        {
                            b = (char)sr.Read();
                            if (b == ';')
                            {
                                if (point == 0)
                                {
                                    nama = temp;
                                }
                                else if (point == 1)
                                {
                                    jam_buka = timeToInt(temp);
                                }
                                else if (point == 2)
                                {
                                    jam_tutup = timeToInt(temp);
                                }
                                point++;
                                temp = "";
                            }
                            else
                            {
                                if (b != ',') temp += b;
                            }
                        }
                        var parse = new StringReader(temp);
                        for (int j = 0; j < temp.Length; j++)
                        {
                            char val = (char)parse.Read();
                            hari_buka.Add(Int32.Parse(val.ToString()));
                        }
                        listOfRuangan.addRuangan(new Ruangan(nama, jam_buka, jam_tutup, hari_buka));
                    }
                    else if (isKelas)
                    {
                        var sr = new StringReader(line);
                        int point = 0;
                        temp = "";
                        hari_buka = new List<int>();
                        for (int k = 0; k < line.Length; k++)
                        {
                            b = (char)sr.Read();
                            if (b == ';')
                            {
                                if (point == 0)
                                {
                                    nama = temp;
                                }
                                else if (point == 1)
                                {
                                    ruangan = temp;
                                }
                                else if (point == 2)
                                {
                                    jam_buka = timeToInt(temp);
                                } else if (point == 3)
                                {
                                    jam_tutup = timeToInt(temp);
                                } else if (point == 4)
                                {
                                    durasi = Int32.Parse(temp);
                                }
                                point++;
                                temp = "";
                            }
                            else
                            {
                                if (b != ',') temp += b;
                            }
                        }
                        var parse = new StringReader(temp);
                        for (int j = 0; j < temp.Length; j++)
                        {
                            char val = (char)parse.Read();
                            hari_buka.Add(Int32.Parse(val.ToString()));
                        }
                        Kelas c = new Kelas(nama, ruangan, jam_buka, jam_tutup, durasi, hari_buka, listOfRuangan);
                        listOfKelas.addKelas(c);
                    }
                }

                KelasManagement ans = new KelasManagement();
                if (user_input.choice == 0)
                {
                    HillClimbing hc = new HillClimbing(listOfKelas, listOfRuangan);
                    ans = hc.getSol();
                    ViewData["Choice"] = "Hill Climbing";
                } else if (user_input.choice == 1)
                {
                    SimulatedAnnealing sa = new SimulatedAnnealing(listOfKelas, listOfRuangan);
                    sa.execute(1000, 0.9f);
                    ans = sa.getSol();
                    ViewData["Choice"] = "Simulated Annealing";
                } else if (user_input.choice == 2)
                {
                    Population p = new Population();
                    p.generatePopulation(200, listOfKelas);
                    GeneticAlgorithm ga = new GeneticAlgorithm(p);
                    for (int ax = 0; ax < 100000; ax++)
                    {
                        if (ga.getSolution().getConflict() == 0)
                        {
                            break;
                        }
                        else
                        {
                            ga.crossover();
                            ga.mutation();
                        }
                    }
                    ans = ga.getSolution();
                    ViewData["Choice"] = "Genetic Algorithm";
                }

                ViewData["tes"] = ans.getConflict();
                int i = 0;
                // Inisialisasi
                for (int j = 7; j < 18; j++)
                {
                    for (int k = 1; k < 6; k++)
                    {
                        ViewData["marker" + j.ToString() + k.ToString()] = 0;
                        ViewData["nama" + j.ToString() + k.ToString()] = "";
                        ViewData["ruangan" + j.ToString() + k.ToString()] = "";
                    }
                }
                // Pengisian kelas di tabel

                foreach (Kelas k in ans.getArrayKelas())
                {
                    ViewData["nama" + i.ToString()] = k.getNama();
                    ViewData["ruangan" + i.ToString()] = k.getNamaRuangan();
                    ViewData["durasi" + i.ToString()] = k.getDurasi();
                    ViewData["hari" + i.ToString()] = k.getHari();
                    ViewData["jam" + i.ToString()] = k.getMulai();
                    for (int z = k.getMulai(); z < (k.getMulai() + k.getDurasi()); z++) {
                        if (ViewData["ruangan" + z.ToString() + k.getHari().ToString()].Equals(""))
                        {
                            ViewData["nama" + z.ToString() + k.getHari().ToString()] = k.getNama();
                            ViewData["ruangan" + z.ToString() + k.getHari().ToString()] = k.getNamaRuangan();
                        } else if (cekRuanganSama(k.getNamaRuangan(), ViewData["ruangan" + z.ToString() + k.getHari().ToString()].ToString())){
                            conflict++;
                            ViewData["marker" + z.ToString() + k.getHari().ToString()] = 1;
                            ViewData["nama" + z.ToString() + k.getHari().ToString()] += " " + k.getNama();
                            ViewData["ruangan" + z.ToString() + k.getHari().ToString()] += " " + k.getNamaRuangan();
                        } else
                        {
                            ViewData["marker" + z.ToString() + k.getHari().ToString()] = 2;
                            ViewData["nama" + z.ToString() + k.getHari().ToString()] += " " + k.getNama();
                            ViewData["ruangan" + z.ToString() + k.getHari().ToString()] += " " + k.getNamaRuangan();
                        }
                    }
                    i++;
                }
                ViewData["Length"] = i;
                ViewData["Message"] = conflict;
            }
            ViewData["Start"] = 1;
            return View();
        }
    }
}

public class UserInput
{
    public int choice { get; set; }
}
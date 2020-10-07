using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;
using System.Xml;

namespace HNLcs
{
    class Program
    {
        public struct Igrac
        {
            public int igracID;
            public string ime;
            public string prezime;
            public int godina;
            public string drzavljanstvo;
            public int klubID;
            public Igrac(int j, string i, string p, int g, string d, int k)
                {
                    igracID = j;
                    ime = i;
                    prezime = p;
                    godina = g;
                    drzavljanstvo = d;
                    klubID = k;
                }
        }
        public struct Klub
        {
            public int klubID;
            public string nazivKluba;
            public Klub(int i, string n)
            {
                klubID = i;
                nazivKluba = n;
            }
        }

        static void Main(string[] args)
        {
            provjeraOtvaranja();
            glavniIzbornik();


            Console.ReadKey();
        }

        public static void provjeraOtvaranja()
        {
            Console.WriteLine("\nProvjera jesu li datoteke postojece......");

            try
            {
                XmlDocument oIgraci = new XmlDocument();
                oIgraci.Load(Path.Combine(Environment.CurrentDirectory + "/xml", "igraci.xml"));

                XmlDocument oKlubovi = new XmlDocument();
                oKlubovi.Load(Path.Combine(Environment.CurrentDirectory + "/xml", "klubovi.xml"));

                XmlDocument oRezultati = new XmlDocument();
                oRezultati.Load(Path.Combine(Environment.CurrentDirectory + "/xml", "rezultati.xml"));
                Console.WriteLine("\nDatoteke su uspjesno ucitane...");
                Console.WriteLine("Nastavak rada programa....");
            }
            catch
            {
                Console.WriteLine("\nDatoteke nisu uspjesno ucitane");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        public static List<Igrac> ucitajIgrace()
        {
            string sXml = "";
            StreamReader oSr = new StreamReader("igraci.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/igraci/igrac");

            List<Igrac> lIgraci = new List<Igrac>();
            foreach (XmlNode oNode in oNodes)
            {
                lIgraci.Add(new Igrac(
                    Convert.ToInt32(oNode.Attributes["igracID"].Value),
                    oNode.Attributes["ime"].Value,
                    oNode.Attributes["prezime"].Value,
                    Convert.ToInt32(oNode.Attributes["godina"].Value),
                    oNode.Attributes["drzavljanstvo"].Value,
                    Convert.ToInt32(oNode.Attributes["klubID"].Value)
                ));
            }

            oSr.Close();

            return lIgraci;
        }
        public static List<Klub> ucitajKlubove()
        {
            string sXml = "";
            StreamReader oSr = new StreamReader("klubovi.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);
            XmlNodeList oNodes = oXml.SelectNodes("//data/klubovi/klub");

            List<Klub> lKlubovi = new List<Klub>();
            foreach (XmlNode oNode in oNodes)
            {
                lKlubovi.Add(new Klub(
                    Convert.ToInt32(oNode.Attributes["klubID"].Value),
                    oNode.Attributes["nazivKluba"].Value
                ));
            }

            oSr.Close();

            return lKlubovi;
        }
        public static string dajKlubPoID(int klub_ID)
        {
            List<Klub> lKlubovi = ucitajKlubove();
            string naziv_Kluba = "";
            foreach(Klub klub in lKlubovi)
            {
                if (klub.klubID == klub_ID)
                {
                    naziv_Kluba = klub.nazivKluba;
                }
            }
            return naziv_Kluba;
        }

        public static void glavniIzbornik()
        {
            Console.Clear();
            Console.WriteLine("\nOdaberite opciju koju želite da program provede:");
            Console.WriteLine("----------------OPCIJE----------------");
            Console.WriteLine("1. --------->Azuriraj klubove. ");
            Console.WriteLine("2. --------->Azuriraj igrace. ");
            Console.WriteLine("3. --------->Prijelaz igraca. ");
            Console.WriteLine("4. --------->Prikaz klubova sa igracima. ");
            Console.WriteLine("5. --------->Odigraj utakmice. ");
            Console.WriteLine("6. --------->Prikazi rang listu. ");
            Console.WriteLine("X  --------->IZLAZ IZ PROGRAMA");
            Console.WriteLine("\nUnesite broj opcije koju zelite provesti.");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);

            int[] dozvoljeni_unosi = { 49, 50, 51, 52, 53, 54, 88 };

            Console.Clear();
            switch (odabir)
            {
                case 49:
                    Console.WriteLine("\nOdabrali ste opciju -->Azuriraj klubove. ");
                    azurirajKlubove();
                    OdabirC();
                    break;
                case 50:
                    Console.WriteLine("\nOdabrali ste opciju  -->Azuriraj igrace. ");
                    azurirajIgrace();
                    OdabirC();
                    break;
                case 51:
                    Console.WriteLine("\nOdabrali ste opciju  -->Prijelaz igraca.");

                    OdabirC();
                    break;
                case 52:
                    Console.WriteLine("\nOdabrali ste opciju  -->Prikaz klubova sa igracima.");

                    OdabirC();
                    break;
                case 53:
                    Console.WriteLine("\nOdabrali ste opciju  -->Odigraj utakmice.");

                    OdabirC();
                    break;
                case 54:
                    Console.WriteLine("\nOdabrali ste opciju  -->Prikazi rang listu.");

                    OdabirC();
                    break;
                case 88: //X
                    System.Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Pritisnite bilo koju tipku za izlaz...");
                    break;
            }
        }
        public static void OdabirC()
        {
            Console.WriteLine("\nPritisnite [Q] za povratak u prethodni izbornik");
            Console.WriteLine("Pritisnite [X] za izlaz iz programa");
            Console.Write("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);

            while (odabir != 81 && odabir != 88)
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno\n");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }
            switch (odabir)
            {
                case 81:
                    glavniIzbornik();
                    break;

                case 88:
                    System.Environment.Exit(1);
                    break;
            }
        }
        public static void ispisiKlubove()
        {
            List<Klub> lKlubovi = ucitajKlubove();
            Console.WriteLine("----Prikaz klubova----");
            var table = new ConsoleTable("R.br.", "ID", "Naziv Kluba");
            int rbr = 1;
            foreach (Klub klub in lKlubovi)
            {
                table.AddRow(rbr++ + ".", klub.klubID, klub.nazivKluba);
            }
            table.Write();
        }
        public static void ispisiIgrace()
        {
            List<Igrac> lIigraci = ucitajIgrace();
            Console.WriteLine("----Prikaz igraca----");
            var table = new ConsoleTable("R.br.", "ID igraca", "Ime", "Prezime", "Godina", "Drzavljanstvo", "Klub");
            int rbr = 1;
            foreach (Igrac igrac in lIigraci)
            {
                table.AddRow(rbr++ + ".", igrac.igracID, igrac.ime, igrac.prezime, igrac.godina, igrac.drzavljanstvo, dajKlubPoID(igrac.klubID));
            }
            table.Write();
            Console.WriteLine();
        }
        public static void dodajIgraca()
        {
            int igracID;
            Console.Clear();
            Console.WriteLine("\n-----Dodaj Igraca-----\n");

            Console.WriteLine("Ime igraca: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Prezime igraca: ");
            string prezime = Console.ReadLine();

            Console.WriteLine("Godina: ");
            int godina = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Drzavljanstvo: ");
            string drzavljanstvo = Console.ReadLine();

            Console.WriteLine("Smjer:");
            List<Klub> lKlubovi = ucitajKlubove();

            int rbr = 1;
            foreach (Klub klub in lKlubovi)
            {
                Console.WriteLine("\t" + rbr++ + ". " + klub.nazivKluba);
            }

            Console.WriteLine("Odaberite redni broj kluba");
            int klub_odabir = Convert.ToInt32(Console.ReadLine());

            int[] dozvoljeni_unosi = { 1, 2, 3 };
            while (!dozvoljeni_unosi.Contains(klub_odabir))
            {
                Console.Write("\nPokreška pri odabiru kluba.Pokušajte ponovno ");
                Console.Write("\nOdaberite redni broj kluba: ");
                klub_odabir = Convert.ToInt32(Console.ReadLine());
            }

            int odabrani_klub_id = lKlubovi[klub_odabir - 1].klubID;

            List<Igrac> lIgraci = ucitajIgrace();

            igracID = lIgraci.Count + 1;

            Igrac igrac = new Igrac(igracID, ime, prezime, godina, drzavljanstvo, odabrani_klub_id);
            
            lIgraci.Add(igrac);

            string sXml = "";
            StreamReader oSr = new StreamReader("igraci.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);

            XmlNode oNodes = oXml.SelectSingleNode("//data/igraci");

            XmlElement xmlIgrac = oXml.CreateElement("igrac");
            xmlIgrac.SetAttribute("igracID", igracID.ToString());
            xmlIgrac.SetAttribute("ime", ime);
            xmlIgrac.SetAttribute("prezime", prezime);
            xmlIgrac.SetAttribute("godina", godina.ToString());
            xmlIgrac.SetAttribute("drzavljanstvo", prezime);
            xmlIgrac.SetAttribute("klubID", odabrani_klub_id.ToString());

            oNodes.AppendChild(xmlIgrac);
            oXml.Save("igraci.xml");

            Console.WriteLine("-----IGRAC USPJESNO DODAN-----");
        }

        public static void obrisiIgraca()
        {
            Console.WriteLine("\n-----Obrisi Igraca-----\n");
            List<Igrac> lIgraci = ucitajIgrace();

            List<int> dozvoljeni_unosi = new List<int>();
            var table = new ConsoleTable("R.br. ", "ID igraca", "Ime", "Prezime", "Godina", "Drzavljanstvo", "Klub");
            
            for (int i = 0; i < lIgraci.Count; i++)
            {
                table.AddRow(
                    i + 1 +".",
                    lIgraci[i].igracID,
                    lIgraci[i].ime,
                    lIgraci[i].prezime,
                    lIgraci[i].godina,
                    lIgraci[i].drzavljanstvo,
                    dajKlubPoID(lIgraci[i].klubID)
                    );
                dozvoljeni_unosi.Add(i + 1);
            }
            table.Write();
            /*
            int id = 0;
            foreach (Igrac igrac in lIgraci)
            {
                id++;
                igrac.igracID = id;
            }
            */
            Console.WriteLine("\nUnesite redni broj igraca kojega zelite izbrisati: ");
            Console.WriteLine("\nRedni broj: ");
            int igrac_odabir = Convert.ToInt32(Console.ReadLine());
            while (!dozvoljeni_unosi.Contains(igrac_odabir))
            {
                Console.Write("\nGreska pri odabiru igraca---Pokusajte ponovno. ");
                Console.Write("\nRedni broj: ");
                igrac_odabir = Convert.ToInt32(Console.ReadLine());
            }

            Igrac igrac = new Igrac(
                lIgraci[igrac_odabir - 1].igracID,
                lIgraci[igrac_odabir - 1].ime,
                lIgraci[igrac_odabir - 1].prezime,
                lIgraci[igrac_odabir - 1].godina,
                lIgraci[igrac_odabir - 1].drzavljanstvo,
                lIgraci[igrac_odabir - 1].klubID
                );

            Console.WriteLine("\nOdabrani igrac: ");
            var odabrani_igrac_table = new ConsoleTable("ID igraca", "Ime", "Prezime", "Godina", "Drzavljanstvo", "Klub");
            odabrani_igrac_table.AddRow(
                igrac.igracID,
                igrac.ime,
                igrac.prezime,
                igrac.godina,
                igrac.drzavljanstvo,
                dajKlubPoID(igrac.klubID)
                );
            odabrani_igrac_table.Write();

            Console.WriteLine("\nPritisnite [Y] ako ste sigurni da želite obrisati odabranoga studenta:");
            Console.WriteLine("Pritisnite bilo koju drugu tipku za prikaz glavnoga izbornika:");
            Console.Write("\nOdabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);

            if (odabir != 89)
            {
                glavniIzbornik();
            }
            else
            {
                lIgraci.RemoveAt(igrac_odabir - 1);

                string sXml = "";
                StreamReader oSr = new StreamReader("igraci.xml");
                using (oSr)
                {
                    sXml = oSr.ReadToEnd();
                }
                XmlDocument oXml = new XmlDocument();
                oXml.LoadXml(sXml);

                XmlNode oNodes = oXml.SelectSingleNode("data/igraci/igrac[@igracID='" + igrac.igracID + "']");
                oNodes.ParentNode.RemoveChild(oNodes);

                oXml.Save("igraci.xml");

                Console.WriteLine("-----IGRAC USPJEŠNO OBRISAN-----");
            }
        }

        public static void azurirajIgrace()
        {
            Console.Clear();
            Console.WriteLine("-----Azuriraj Igrace-----\n");
            Console.WriteLine("Odaberite da li zelite...");
            Console.WriteLine("         1------Izbrisati igraca");
            Console.WriteLine("         2------Dodati igraca");
            Console.WriteLine("Unesite broj uz zeljenu opciju koju zelite provesti");
            Console.Write("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);

            int[] dozvoljeni_unosi = { 49, 50, 51, 52, 53, 88 };

            while (!dozvoljeni_unosi.Contains(odabir))
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }

            switch (odabir)
            {
                case 49: //1
                    obrisiIgraca();
                    break;

                case 50: //2
                    dodajIgraca();
                    break;
            }
        }
        public static void dodajKlub()
        {
            Console.WriteLine("\n-----DODAJ KLUB-----\n");

            int klubID = 0;

            Console.WriteLine("Naziv kluba:");
            string nazivKluba = Console.ReadLine();

            Klub klub = new Klub(klubID, nazivKluba);
            List<Klub> lKlubovi = ucitajKlubove();

            klubID = lKlubovi.Count() + 1;

            if (lKlubovi.Count >= 10)
            {
                Console.WriteLine("KLUBOVA NE SMIJE BITI VISE OD 10");
                Console.WriteLine("Odaberite da li zelite...");
                Console.WriteLine("         1------Izbrisati klub");
                Console.WriteLine("         2------Ponovno pokretanje programa");
                Console.WriteLine("Unesite broj uz zeljenu opciju koju zelite provesti");
                Console.Write("\nVaš odabir: ");
                int odabir = Convert.ToInt32(Console.ReadKey().Key);

                int[] dozvoljeni_unosi = { 49, 50, 51, 52, 53, 88 };

                while (!dozvoljeni_unosi.Contains(odabir))
                {
                    Console.WriteLine("\nKrivi odabir, pokušajte ponovno");
                    Console.Write("\nVaš odabir: ");
                    odabir = Convert.ToInt32(Console.ReadKey().Key);
                }

                switch (odabir)
                {
                    case 49: //1
                        obrisiKlub();
                        break;

                    case 50: //2
                        glavniIzbornik();
                        break;
                }
            }

            lKlubovi.Add(klub);

            string sXml = "";
            StreamReader oSr = new StreamReader("klubovi.xml");
            using (oSr)
            {
                sXml = oSr.ReadToEnd();
            }
            XmlDocument oXml = new XmlDocument();
            oXml.LoadXml(sXml);

            XmlNode oNodes = oXml.SelectSingleNode("//data/klubovi");

            XmlElement xmlKlub = oXml.CreateElement("klub");
            xmlKlub.SetAttribute("klubID", klubID.ToString());
            xmlKlub.SetAttribute("nazivKluba", nazivKluba);

            oNodes.AppendChild(xmlKlub);

            oXml.Save("klubovi.xml");

            Console.WriteLine("------KLUB USPJESNO DODAN-----");
        }

        public static void obrisiKlub()
        {
            Console.WriteLine("-----OBRISI KLUB-----");

            Console.WriteLine("NAPOMENA!");
            Console.WriteLine("Brisanjem kluba ce se obrisati svi igraci koji igraju za taj klub.");

            List<Klub> lKlubovi = ucitajKlubove();
            List<int> dozvoljeni_unosi = new List<int>();

            List<Igrac> lIgraci = ucitajIgrace();

            var table = new ConsoleTable("R.br.", "ID kluba", "Naziv Kluba");
            for(int i=0; i<lKlubovi.Count; i++)
            {
                table.AddRow(
                    i + 1 + ".",
                    lKlubovi[i].klubID,
                    lKlubovi[i].nazivKluba
                    );

                dozvoljeni_unosi.Add(i + 1);
            }
            table.Write();

            Console.WriteLine("Unesite redni broj kluba koji zelite obrisati: ");
            Console.WriteLine("\nRedni broj:");
            int klub_odabir = Convert.ToInt32(Console.ReadLine());
            while (!dozvoljeni_unosi.Contains(klub_odabir))
            {
                Console.Write("\nPokreška pri odabiru kluba.Pokušajte ponovno. ");
                Console.Write("\nRedni broj: ");
                klub_odabir = Convert.ToInt32(Console.ReadLine());
            }
            Klub klub = new Klub(
                lKlubovi[klub_odabir - 1].klubID,
                lKlubovi[klub_odabir - 1].nazivKluba
                );
            Console.WriteLine("\nOdabrali ste klub:");
            var odabrani_klub_table = new ConsoleTable("ID kluba", "Naziv Kluba");
            odabrani_klub_table.AddRow(
                klub.klubID,
                klub.nazivKluba
                );
            odabrani_klub_table.Write();

            Console.WriteLine("\nPritisnite [Y] ako ste sigurni da želite obrisati odabranoga studenta:");
            Console.WriteLine("Pritisnite bolo koju drugu tipku za prikaz glavnoga izbornika:");
            Console.Write("\nOdabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);

            /*
             89 - Y
             */
            if (odabir != 89)
            {
                glavniIzbornik();
            }
            else
            {
                lKlubovi.RemoveAt(klub_odabir - 1);

                string sXml = "";
                StreamReader oSr = new StreamReader("klubovi.xml");
                using (oSr)
                {
                    sXml = oSr.ReadToEnd();
                }
                XmlDocument oXml = new XmlDocument();
                oXml.LoadXml(sXml);

                XmlNode oNodes = oXml.SelectSingleNode("data/klubovi/klub[@klubID='" + klub.klubID + "']");
                oNodes.ParentNode.RemoveChild(oNodes);
                oXml.Save("klubovi.xml");

                Console.WriteLine("*** STUDENT USPJEŠNO OBRISAN ***");
            }
        }
        public static void azurirajKlubove()
        {
            Console.Clear();
            Console.WriteLine("-----Azuriraj klubove-----\n");
            Console.WriteLine("Odaberite da li zelite...");
            Console.WriteLine("         1------Izbrisati klub");
            Console.WriteLine("         2------Dodati klub");
            Console.WriteLine("Unesite broj uz zeljenu opciju koju zelite provesti");
            Console.Write("\nVaš odabir: ");
            int odabir = Convert.ToInt32(Console.ReadKey().Key);

            int[] dozvoljeni_unosi = { 49, 50, 51, 52, 53, 88 };

            while (!dozvoljeni_unosi.Contains(odabir))
            {
                Console.WriteLine("\nKrivi odabir, pokušajte ponovno");
                Console.Write("\nVaš odabir: ");
                odabir = Convert.ToInt32(Console.ReadKey().Key);
            }

            switch (odabir)
            {
                case 49: //1
                    obrisiKlub();
                    break;

                case 50: //2
                    dodajKlub();
                    break;
            }
        }
    }
}

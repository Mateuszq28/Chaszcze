﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;

//do formatowania dat
using System.Globalization;

namespace Chaszcze
{
    class Zarzadzanie
    {
        //Do rozpoznawania formatu daty
        static CultureInfo provider = CultureInfo.InvariantCulture;
        static string formatData = "dd.MM.yyyy HH:mm:ss";
        //static string formatGodzina = "HH:mm";

        //Zmienne podawane na początku gry
        static public string nazwaPatrolu;
        static public DateTime minutaStartowa;
        static public DateTime czasRozpoczecia;

        //Ważne podczas gry
        static public List<string> kodyLampionow = new List<string>();
        static public bool czyGraTrwa = false;

        //Liczone dopiero na koniec gry
        static public DateTime minutaZakonczenia;
        static public DateTime czasZakonczenia;
        static public TimeSpan calkowityCzas;
        static public int karne;


        //Stałe wartości
        //Nazwa pliku do zapisywania savów z gry
        static string nazwaPliku = "zapis_chaszcze.txt";
        private static TimeSpan limitCzasu = TimeSpan.Parse("01:45"); //Godzina progowa!
        private static TimeSpan limitSpoznien = TimeSpan.Parse("02:30"); //Godzina progowa!
        //liczba wszystkich lampionów
        public const int liczbaPunktow = 15;
        //liczba stowarzyszy +1
        private const int liczbaLampNaPunkt = 10;
        //Wszystkie kody do lampionów
        //Pierwsza kolumna to wlasciwy punkt, pozostale to stowarzysze
        private static string[,] wzorcowka = new string[liczbaPunktow, liczbaLampNaPunkt] {  { "1-VN", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI" },
                                                                                            { "2-WK", "2-JL", "2-JL", "2-JL", "2-JL", "2-JL", "2-JL", "2-JL", "2-JL", "2-JL" },
                                                                                            { "3-NA", "3-DE", "3-DE", "3-DE", "3-DE", "3-DE", "3-DE", "3-DE", "3-DE", "3-DE" },
                                                                                            { "4-SR", "4-GO", "4-GO", "4-GO", "4-GO", "4-GO", "4-GO", "4-GO", "4-GO", "4-GO" },
                                                                                            { "5-MZ", "5-KF", "5-KF", "5-KF", "5-KF", "5-KF", "5-KF", "5-KF", "5-KF", "5-KF" },
                                                                                            { "6-BS", "6-PY", "6-PY", "6-PY", "6-PY", "6-PY", "6-PY", "6-PY", "6-PY", "6-PY" },
                                                                                            { "7-NJ", "7-BA", "7-UD", "7-UD", "7-UD", "7-UD", "7-UD", "7-UD", "7-UD", "7-UD" },
                                                                                            { "8-KS", "8-LT", "8-LT", "8-LT", "8-LT", "8-LT", "8-LT", "8-LT", "8-LT", "8-LT" },
                                                                                            { "9-OT", "9-HJ", "9-CI", "9-CI", "9-CI", "9-CI", "9-CI", "9-CI", "9-CI", "9-CI" },
                                                                                            { "10-GL", "10-EJ", "10-EJ", "10-EJ", "10-EJ", "10-EJ", "10-EJ", "10-EJ", "10-EJ", "10-EJ" },
                                                                                            { "11-KL", "11-MC", "11-MC", "11-MC", "11-MC", "11-MC", "11-MC", "11-MC", "11-MC", "11-MC" },
                                                                                            { "12-PB", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ" },
                                                                                            { "13-NO", "13-RH", "13-RH", "13-RH", "13-RH", "13-RH", "13-RH", "13-RH", "13-RH", "13-RH" },
                                                                                            { "14-UF", "14-JE", "14-JE", "14-JE", "14-JE", "14-JE", "14-JE", "14-JE", "14-JE", "14-JE" },
                                                                                            { "15-SH", "15-MT", "15-MT", "15-MT", "15-MT", "15-MT", "15-MT", "15-MT", "15-MT", "15-MT" }};


        //Resetuje grę - czyści zmienne
        static public void reset()
        {
            //Zmienne podawane na początku gry
            nazwaPatrolu = null;
            minutaStartowa = DateTime.MinValue;
            czasRozpoczecia = DateTime.MinValue;

            //Ważne podczas gry
            kodyLampionow.Clear();
            czyGraTrwa = false;

            //Liczone dopiero na koniec gry
            czasZakonczenia = DateTime.MinValue;
            minutaZakonczenia = DateTime.MinValue;
            calkowityCzas = TimeSpan.MinValue;
            karne = 0;
        }


        //Zapisywanie gry do pliku
        //WERSJA TEKSTOWA
        static public string SaveGeme()
        {
            string zawartocPliku = nazwaPatrolu;
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nazwaPliku);
            
            if (czyGraTrwa)
            {
                zawartocPliku += "\n1";
            }
            else
            {
                zawartocPliku += "\n0";
            }
            zawartocPliku += "\n" + minutaStartowa.ToString(formatData, provider);
            zawartocPliku += "\n" + czasRozpoczecia.ToString(formatData, provider);
            zawartocPliku += "\n" + minutaZakonczenia.ToString(formatData, provider);
            zawartocPliku += "\n" + czasZakonczenia.ToString(formatData, provider);
            zawartocPliku += "\n" + karne;
            zawartocPliku += "\n" + calkowityCzas.ToString();

            foreach (string kod in kodyLampionow)
            {
                zawartocPliku += "\n" + kod;
            }

            using (var writer = File.CreateText(backingFile))
            {
                writer.Write(zawartocPliku);
            }

            return zawartocPliku;
        }


        //Funkcja wczytuje grę z pliku tekstowego o nazwie 'nazwaPliku'
        //WERSJA TEKSTOWA
        public static string ReadGame()
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nazwaPliku);
            string zawartoscPliku = "";

            if (backingFile == null || !File.Exists(backingFile))
            {
                return null;
            }

            using (var reader = new StreamReader(backingFile, true))
            {
                string line;
                if ((line = reader.ReadLine()) != null)
                {
                    nazwaPatrolu = line;
                    zawartoscPliku = line;
                }
                if ((line = reader.ReadLine()) != null)
                {
                    if (line == "1") czyGraTrwa = true;
                    else czyGraTrwa = false;

                    zawartoscPliku += "\n" + line;
                }
                if ((line = reader.ReadLine()) != null)
                {
                    zawartoscPliku += "\n" + line;
                    minutaStartowa = DateTime.ParseExact(line, formatData, provider);
                }
                if ((line = reader.ReadLine()) != null)
                {
                    zawartoscPliku += "\n" + line;
                    czasRozpoczecia = DateTime.ParseExact(line, formatData, provider);
                }
                if ((line = reader.ReadLine()) != null)
                {
                    zawartoscPliku += "\n" + line;
                    minutaZakonczenia = DateTime.ParseExact(line, formatData, provider);
                }
                if ((line = reader.ReadLine()) != null)
                {
                    zawartoscPliku += "\n" + line;
                    czasZakonczenia = DateTime.ParseExact(line, formatData, provider);
                }
                if ((line = reader.ReadLine()) != null)
                {
                    zawartoscPliku += "\n" + line;
                    karne = Int32.Parse(line);
                }
                if ((line = reader.ReadLine()) != null)
                {
                    zawartoscPliku += "\n" + line;
                    calkowityCzas = TimeSpan.Parse(line);
                }

                kodyLampionow.Clear();
                while ((line = reader.ReadLine()) != null)
                {
                    zawartoscPliku += "\n" + line;
                    kodyLampionow.Add(line);
                }
            }

            return zawartoscPliku;
        }


        //Ustaw kolory zgodnie z listą
        public static void ustawKolory()
        {
            //ile znaleziono odpowiedzi do danego punktu
            int znaleziono;


            if (czyGraTrwa)
            {
                for (int i = 0; i < liczbaPunktow; i++)
                {
                    znaleziono = kodyLampionow.Count(x => x.StartsWith(i + 1 + "-"));
                    if (znaleziono == 0)
                    {
                        Akcje.zmienKolor((i + 1).ToString(), "light_gray");
                    }
                    else if (znaleziono == 1)
                    {
                        Akcje.zmienKolor((i + 1).ToString(), "green");
                    }
                    else
                    {
                        Akcje.zmienKolor((i + 1).ToString(), "yellow");
                    }
                }
            }
            else
            {
                //ostatnia odpowiedź na karcie odpowiedzi dla danego punktu
                string kod;
                //odwracamy listę, bo brana jest pod uwagę ostatnia odpowiedź
                kodyLampionow.Reverse();

                //Sprawdzanie poprawności kodów lampionów
                for (int i = 0; i < liczbaPunktow; i++)
                {
                    znaleziono = kodyLampionow.Count(x => x.StartsWith(i + 1 + "-"));

                    if (znaleziono == 0)
                    {
                        //Nie odnotowano żadnego kodu
                        Akcje.zmienKolor((i + 1).ToString(), "black");
                    }
                    else
                    {
                        kod = kodyLampionow.Find(x => x.StartsWith(i + 1 + "-"));

                        if (kod == wzorcowka[i, 0])
                        {
                            //Prawidłowy lampion
                            Akcje.zmienKolor((i + 1).ToString(), "green");
                        }
                        else
                        {
                            for (int j = 1; j < liczbaLampNaPunkt; j++)
                            {
                                if (kod == wzorcowka[i, j])
                                {
                                    //Stowarzyszony
                                    Akcje.zmienKolor((i + 1).ToString(), "orange");
                                    break;
                                }
                                else if (j == 9)
                                {
                                    //Mylny
                                    Akcje.zmienKolor((i + 1).ToString(), "red");
                                }

                            }
                        }
                    }
                }
                //Odwracamy listę do normalnej kolejności
                kodyLampionow.Reverse();
            }
        }


        //Oblicza ile minut minęło w interwale czasu
        static int ileMinut(TimeSpan czas)
        {
            return czas.Days * 24 * 60 + czas.Hours * 60 + czas.Minutes;
        }


        //Kończy grę
        static public string zakonczenie()
        {
            //ile znaleziono odpowiedzi do danego punktu
            int znaleziono;
            //ostatnia odpowiedź na karcie odpowiedzi dla danego punktu
            string kod;

            czyGraTrwa = false;
            karne = 0;
            //odwracamy listę, bo brana jest pod uwagę ostatnia odpowiedź
            kodyLampionow.Reverse();
            czasZakonczenia = DateTime.Now;
            calkowityCzas = czasZakonczenia - czasRozpoczecia;
            minutaZakonczenia = minutaStartowa + calkowityCzas;

            //Punkty karne za spóźnienie
            if (calkowityCzas > limitCzasu)
            {
                if (calkowityCzas > limitSpoznien)
                {
                    karne += ileMinut(limitSpoznien - limitCzasu) + ileMinut(calkowityCzas - limitSpoznien) * 10;
                }
                else
                {
                    karne += ileMinut(calkowityCzas - limitCzasu);
                }
            }

            //Sprawdzanie poprawności kodów lampionów
            for (int i = 0; i < liczbaPunktow; i++)
            {
                znaleziono = kodyLampionow.Count(x => x.StartsWith(i + 1 + "-"));

                if (znaleziono == 0)
                {
                    //Nie odnotowano żadnego kodu
                    karne += 90;
                    Akcje.zmienKolor((i + 1).ToString(), "black");
                }
                else
                {
                    //Punkty karne za naniesione poprawki
                    karne += (znaleziono - 1) * 10;
                    kod = kodyLampionow.Find(x => x.StartsWith(i + 1 + "-"));

                    if (kod == wzorcowka[i, 0])
                    {
                        //Prawidłowy lampion
                        Akcje.zmienKolor((i + 1).ToString(), "green");
                    }
                    else
                    {
                        for (int j = 1; j < liczbaLampNaPunkt; j++)
                        {
                            if (kod == wzorcowka[i, j])
                            {
                                //Stowarzyszony
                                karne += 25;
                                Akcje.zmienKolor((i + 1).ToString(), "orange");
                                break;
                            }
                            else if (j == 9)
                            {
                                //Mylny
                                karne += 90 + 60;
                                Akcje.zmienKolor((i + 1).ToString(), "red");
                            }

                        }
                    }
                }
            }
            //Odwracamy listę do normalnej kolejności
            kodyLampionow.Reverse();
            //Zapisz grę
            return SaveGeme();
        }





        //Zapisywanie gry do pliku
        //WERSJA BINARNA
        /*
        static public void SaveGemeAsync()
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nazwaPliku);
            using (BinaryWriter writer = new BinaryWriter(File.Open(backingFile, FileMode.Create)))
            {
                writer.Write(Zarzadzanie.nazwaPatrolu);
                writer.Write(Zarzadzanie.czyGraTrwa);
                writer.Write(Zarzadzanie.minutaStartowa.ToBinary());
                writer.Write(Zarzadzanie.czasRozpoczecia.ToBinary());
            }
        }
        */


        //Funkcja wczytuje grę z pliku tekstowego o nazwie 'nazwaPliku'
        //WERSJA BINARNA
        /*
        public static bool ReadGameAsync()
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nazwaPliku);

            if (backingFile == null || !File.Exists(backingFile))
            {
                return false;
            }

            using (BinaryReader reader = new BinaryReader(File.Open(backingFile, FileMode.Open)))
            {
                string line;
                if ((line = reader.ReadString()) != null)
                {
                    Zarzadzanie.nazwaPatrolu = line;
                    Zarzadzanie.czyGraTrwa = reader.ReadBoolean();
                    Zarzadzanie.minutaStartowa = DateTime.FromBinary(reader.ReadInt64());
                    Zarzadzanie.czasRozpoczecia = DateTime.FromBinary(reader.ReadInt64());
                }
            }

            return true;
        }*/
    }
}
using System;
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

namespace Chaszcze
{
    class Zarzadzanie
    {
        static public List<String> kodyLampionow = new List<String>();
        static public String nazwaPatrolu;
        static public DateTime minutaStartowa;
        static public DateTime czasRozpoczecia;

        static public DateTime czasZakonczenia;
        static public DateTime minutaZakonczenia;
        static public int karne;
        static public TimeSpan calkowityCzas;
        static public bool czyGraTrwa = false;

        static TimeSpan limitCzasu = TimeSpan.Parse("02:00");
        static TimeSpan limitSpoznien = TimeSpan.Parse("00:45");

        static public void dekonstruktor()
        {
            kodyLampionow.Clear();
            //nazwaPatrolu = null;
            //minutaStartowa = DateTime.MinValue;
            //czasRozpoczecia = DateTime.MinValue;
            czasZakonczenia = DateTime.MinValue;
            minutaZakonczenia = DateTime.MinValue;
            karne = 0;
            calkowityCzas = TimeSpan.MinValue;
            czyGraTrwa = true;

            for (int i = 1; i <= 12; i++)
            {
                Akcje.zmienKolor(i.ToString(),"");
            }
        }

        static public void zapisz()
        {
            StreamWriter sw = new StreamWriter("zapis_chaszcze.txt");
        }

        static public void wczytaj()
        {

        }

        static public void zakonczenie()
        {
            czyGraTrwa = false;
            int znaleziono;
            karne = 0;
            kodyLampionow.Reverse();
            string kod;

            czasZakonczenia = DateTime.Now;
            calkowityCzas = czasZakonczenia - czasRozpoczecia;
            minutaZakonczenia = minutaStartowa + calkowityCzas;

            if (calkowityCzas > limitCzasu)
            {
                if (calkowityCzas > limitSpoznien)
                {
                    karne += (limitSpoznien - limitCzasu).Minutes + (calkowityCzas - limitSpoznien).Minutes * 10;
                }
                else
                {
                    karne += (calkowityCzas - limitCzasu).Minutes;
                }
            }

            //Pierwsza kolumna to wlasciwy punkt, pozostale to stowarzysze
            string[,] wzorcowka = new string[12,10] {   { "1-VN", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI" },
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
                                                        { "12-PB", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ", "12-OZ" }};

            for (int i = 0; i < 12; i++)
            {
                znaleziono = kodyLampionow.Count(x => x.StartsWith(i+1 + "-"));

                if (znaleziono == 0)
                {
                    karne += 90;
                    Akcje.zmienKolor((i + 1).ToString(), "black");
                }
                else
                {
                    karne += (znaleziono - 1) * 10;
                    kod = kodyLampionow.Find(x => x.StartsWith(i+1 + "-"));

                    if (kod == wzorcowka[i,0])
                    {
                        Akcje.zmienKolor((i + 1).ToString(), "green");
                    }
                    else
                    {
                        for (int j = 1; j < 10; j++)
                        {
                            if (kod == wzorcowka[i,j])
                            {
                                karne += 25;
                                Akcje.zmienKolor((i + 1).ToString(), "orange");
                                break;
                            }
                            else if (j == 9)
                            {
                                karne += 90 + 60;
                                Akcje.zmienKolor((i + 1).ToString(), "red");
                            }

                        }
                    }
                }
            }
            
                
        }
    }
}
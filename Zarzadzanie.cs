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
        static public bool czyGraTrwa;

        static TimeSpan limitCzasu = TimeSpan.Parse("01:20");
        static TimeSpan limitSpoznien = TimeSpan.Parse("02:30");

        static public void dekonstruktor()
        {
            kodyLampionow.Clear();
            nazwaPatrolu = null;
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
            string[,] wzorcowka = new string[12,10] {   { "1-BB", "1-AA", "1-AA", "1-AA", "1-AA", "1-AA", "1-AA", "1-AA", "1-AA", "1-AA" },
                                                        { "2-BB", "2-AA", "2-AA", "2-AA", "2-AA", "2-AA", "2-AA", "2-AA", "2-AA", "2-AA" },
                                                        { "3-BB", "3-AA", "3-AA", "3-AA", "3-AA", "3-AA", "3-AA", "3-AA", "3-AA", "3-AA" },
                                                        { "4-BB", "4-AA", "4-AA", "4-AA", "4-AA", "4-AA", "4-AA", "4-AA", "4-AA", "4-AA" },
                                                        { "5-BB", "5-AA", "5-AA", "5-AA", "5-AA", "5-AA", "5-AA", "5-AA", "5-AA", "5-AA" },
                                                        { "6-BB", "6-AA", "6-AA", "6-AA", "6-AA", "6-AA", "6-AA", "6-AA", "6-AA", "6-AA" },
                                                        { "7-BB", "7-AA", "7-AA", "7-AA", "7-AA", "7-AA", "7-AA", "7-AA", "7-AA", "7-AA" },
                                                        { "8-BB", "8-AA", "8-AA", "8-AA", "8-AA", "8-AA", "8-AA", "8-AA", "8-AA", "8-AA" },
                                                        { "9-BB", "9-AA", "9-AA", "9-AA", "9-AA", "9-AA", "9-AA", "9-AA", "9-AA", "9-AA" },
                                                        { "10-BB", "10-AA", "10-AA", "10-AA", "10-AA", "10-AA", "10-AA", "10-AA", "10-AA", "10-AA" },
                                                        { "11-BB", "11-AA", "11-AA", "11-AA", "11-AA", "11-AA", "11-AA", "11-AA", "11-AA", "11-AA" },
                                                        { "12-BB", "12-AA", "12-AA", "12-AA", "12-AA", "12-AA", "12-AA", "12-AA", "12-AA", "12-AA" }};

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
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
        //Zmienne podawane na początku gry
        static public String nazwaPatrolu;
        static public DateTime minutaStartowa;
        static public DateTime czasRozpoczecia;

        //Ważne podczas gry
        static public List<String> kodyLampionow = new List<String>();
        static public bool czyGraTrwa = false;

        //Liczone dopiero na koniec gry
        static public DateTime czasZakonczenia;
        static public DateTime minutaZakonczenia;
        static public TimeSpan calkowityCzas;
        static public int karne;


        //Stałe wartości
        private static TimeSpan limitCzasu = TimeSpan.Parse("02:00");
        private static TimeSpan limitSpoznien = TimeSpan.Parse("00:45");
        //Wszystkie kody do lampionów
        //Pierwsza kolumna to wlasciwy punkt, pozostale to stowarzysze
        private static string[,] wzorcowka = new string[12, 10] {  { "1-VN", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI", "1-TI" },
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


        //Ustaw kolory zgodnie z listą
        public static void ustawKolory()
        {
            //ile znaleziono odpowiedzi do danego punktu
            int znaleziono;

            for (int i = 1; i <= 12; i++)
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
                    Akcje.zmienKolor((i + 1).ToString(), "orange");
                }
            }
        }


        //Kończy grę
        static public void zakonczenie()
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
                    karne += (limitSpoznien - limitCzasu).Minutes + (calkowityCzas - limitSpoznien).Minutes * 10;
                }
                else
                {
                    karne += (calkowityCzas - limitCzasu).Minutes;
                }
            }

            //Sprawdzanie poprawności kodów lampionów
            for (int i = 0; i < 12; i++)
            {
                znaleziono = kodyLampionow.Count(x => x.StartsWith(i+1 + "-"));

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
                    kod = kodyLampionow.Find(x => x.StartsWith(i+1 + "-"));

                    if (kod == wzorcowka[i,0])
                    {
                        //Prawidłowy lampion
                        Akcje.zmienKolor((i + 1).ToString(), "green");
                    }
                    else
                    {
                        for (int j = 1; j < 10; j++)
                        {
                            if (kod == wzorcowka[i,j])
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
            //Zapisz grę
            Akcje.SaveCountAsync();
        }
    }
}

/*
 * 
 * This file is part of TyCoDim.
 * TyCoDim is a program to calculate reinforced concrete beams
 * 
 * Copyright (C) 2018 Mathias Schöpf <schoepf.mathias@gmail.com>
 *
 * TyCoDim is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * TyCoDim is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with TyCoDim. If not, see <http://www.gnu.org/licenses/>.
 * 
 */


using System;
using System.Collections.Generic;

namespace TyCoDim_Library
{
    public static class Calc
    {
        //Konstanten
        public const double YC = 1.5; //Sicherheitsbeiwert Beton
        public const double YS = 1.15; //Sicherheitsbeiwert Stahl
        public const int DsT = 26; //Durchmesser Bewehrung Träger = 26mm (mm)
        public const int DsP = 16; //Durchmesser Bewehrung Platte = 16mm (mm)
        public const int DBue = 8; //Durchmesser Bügel (mm)

        //Werte aus der Tabelle
        //Beton
        private static int FCK = 25; //Druckfestigkeit Zylinder/Würfel (N/mm²)
        private static double Kappa = 0.8095;
        private static double Lamda = 0.4160;
        //Stahl
        private static int FYK = 550; //Streckgrenze Stahl (N/mm²)
        private static double MdG = 0.362;
        //Größtkorn
        private static int Gk = 22; //Größtkorn (mm)

        //Eingebbare Werte
        public static double BT = 0; //Breite des Trägers (m)
        public static double HT = 0; //Höhe des Trägers (m)
        public static double MEd = 0; //Bemessungsmoment (kN*m)
        public static double Cnom = 0; //Betondeckung (cm)

        //Berechnungswerte
        public static double FCD = 0;
        public static double FYD = 0;

        public static double DT = 0;
        public static double D1 = 0;
        public static double Md = 0; //M = μ
        public static double Xi = 0;
        public static double Zeta = 0;
        public static double Aserf = 0; //Erforderliche Fläche

        //Bewehrung
        public static int[][] Bewehrung = new int[3][]; //2D-Array für die Bewehrungsauswahl
        public static bool Bewehrungen = false; //False wenn keine Bewehrungen ermittelt werden konnten
        public static bool MdGU = false; //True wenn MdG überschritten wurde

        static Calc()
        {
            //Initialisierung für das 2D-Array
            Bewehrung[0] = new int[2];
            Bewehrung[1] = new int[2];
            Bewehrung[2] = new int[2];
        }

        //Wendelt eine Index-Angabe in den entsprechenden Querschnitt um
        private static int AsIndexToCrossSection(int Index)
        {
            int Querschnitt = 0;
            switch(Index)
            {
                case  0: Querschnitt =  8; break;
                case  1: Querschnitt = 10; break;
                case  2: Querschnitt = 12; break;
                case  3: Querschnitt = 14; break;
                case  4: Querschnitt = 16; break;
                case  5: Querschnitt = 20; break;
                case  6: Querschnitt = 26; break;
                case  7: Querschnitt = 30; break;
                case  8: Querschnitt = 36; break;
                case  9: Querschnitt = 40; break;
                case 10: Querschnitt = 50; break;
                default: Querschnitt =  8; break;
            }
            return Querschnitt;
        }
        
        //Ermittelt aus den möglichen Beton-Normen die entsprechenden Materialwerte
        public static void GetBeton(string Auswahl)
        {
            switch (Auswahl)
            {
                case "C12/15":  FCK = 12; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C16/20":  FCK = 16; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C20/25":  FCK = 20; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C25/30":  FCK = 25; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C30/37":  FCK = 30; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C35/45":  FCK = 35; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C40/50":  FCK = 40; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C45/55":  FCK = 45; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C50/60":  FCK = 50; Kappa = 0.8095; Lamda = 0.4160; break;
                case "C55/67":  FCK = 55; Kappa = 0.7442; Lamda = 0.3926; break;
                case "C60/75":  FCK = 60; Kappa = 0.6936; Lamda = 0.3768; break;
                case "C70/85":  FCK = 70; Kappa = 0.6268; Lamda = 0.3599; break;
                case "C80/95":  FCK = 80; Kappa = 0.5978; Lamda = 0.3547; break;
                case "C90/105": FCK = 90; Kappa = 0.5833; Lamda = 0.3529; break;
                default:        FCK = 25; Kappa = 0.8095; Lamda = 0.4160; break;
            }
        }

        //Ermittelt aus den möglichen Stahl-Normen die entsprechenden Materialwerte
        public static void GetStahl(string Auswahl)
        {
            switch (Auswahl)
            {
                case "S420": FYK = 420; MdG = 0.387; break;
                case "S500": FYK = 500; MdG = 0.371; break;
                case "S550": FYK = 550; MdG = 0.362; break;
                case "S600": FYK = 600; MdG = 0.353; break;
                default:     FYK = 550; MdG = 0.362; break;
            }
        }

        //Ermittelt aus den möglichen Größtkornbezeichnungen den entsprechenden Durchmesser
        public static void GetGk(string Auswahl)
        {
            switch (Auswahl)
            {
                case "16mm": Gk = 16; break;
                case "22mm": Gk = 22; break;
                case "32mm": Gk = 32; break;
                default:     Gk = 22; break;
            }
        }

        //Überprüft, ob genug Platz zwischen den Stäben besteht
        private static bool CheckDistance(int st, int dS)
        {
            double e = 0;
            try
            {
                e = (100 * BT - 2 * Cnom - 0.24 * DBue - 0.12 * dS * st) / (st - 1);
            }
            catch { }

            if ((e >= 2) && (e >= 0.1 * dS) && (e >= 0.1 * Gk)) return true;
            else return false;
        }

        //Berechnet einen Stahlbetonträger
        public static void CalculateT()
        {
            try
            {
                //Setzt Ausgabewerte zurück
                Bewehrung[0][0] = 0; Bewehrung[0][1] = 0;
                Bewehrung[1][0] = 0; Bewehrung[1][1] = 0;
                Bewehrung[2][0] = 0; Bewehrung[2][1] = 0;
                Aserf = 0;
                MdGU = false;

                //Berechnet FCD und FYD + Umrechnung auf kN/m² bzw. kN/cm²
                FCD = FCK / YC * 1000;
                FYD = FYK / YS * 0.1;

                //Berechnet DT + Umrechnung auf m
                DT = HT - Cnom * 0.01 - (0.0012 * DsT) / 2 - 0.0012 * DBue;
                //Berechnet D1 + Umrechnung auf cm
                D1 = Cnom + 0.012 * DBue + (0.012 * DsT) / 2;
                Md = MEd / (BT * DT * DT * FCD);
                Xi = 1.202 * (1 - Math.Sqrt(1 - 4 * (Lamda / Kappa) * Md));
                Zeta = 0.5 * (1 + Math.Sqrt(1 - 4 * (Lamda / Kappa) * Md));
                Aserf = Math.Round(MEd / (Zeta * DT * FYD), 2);

                //Falls µd (=Md) über dem Grenzwert liegt, wird abgebrochen
                if (Md > MdG)
                {
                    MdGU = true;
                    Bewehrungen = false;
                    return;
                }

                //Sollte die erforderliche Fläche 0 sein, wird 
                if (Aserf <= 0)
                {
                    Bewehrungen = false;
                    return;
                }

                //Nun wird nach einer passenden Bewehrung gesucht
                for (int d = 0, stk = 1, b = 0; b < 3;)
                {
                    int dS = AsIndexToCrossSection(d);
                    if (GetAs(stk, dS) >= Aserf) //Ist die erforderliche Fläche groß genug?
                    {
                        if (CheckDistance(stk, dS)) //Ja! Überprüfe nun die Abstände zw. den Stäben
                        {
                            //Die Abstände sind groß genug, wir haben eine Bewehrung gefunden!
                            Bewehrung[b][0] = dS;
                            Bewehrung[b][1] = stk;
                            b++;
                            d++;
                            stk = 1;
                        }
                        else
                        {
                            //Die Abstände passen nicht, gehe zum nächsten Durchmesser
                            d++;
                            stk = 1;
                        }
                    }
                    else
                    {
                        if (stk >= 10) //Nein! Sind wir schon bei der maximalen Stückzahl?
                        {
                            if (d >= 10) //Ja! Sind wir schon beim maximalen Durchmesser?
                            {
                                //Ja! Damit können keine weiteren Bewehrungen mehr gefunden werden
                                Bewehrungen = false;
                                return;
                            }
                                //Nein! Gehe zum nächsten Durchmesser und fang wieder bei Eins an!
                            d++;
                            stk = 1;
                        }
                        else
                        {
                            //Nein! Erhöhe die Stückzahl!
                            stk++;
                        }
                    }
                }
                Bewehrungen = true;
            }
            catch { }
        }

        //Noch nicht implementiert
        private static void CalculateP()
        {
            try
            {

            }
            catch { }
        }

        //Berechne die Querschnittsfläche aus Durchmesser und Stückzahl
        public static double GetAs(int stk, int dS)
        {
            return Math.Round(Math.Pow(dS / 2, 2) * Math.PI * stk * 0.01, 2);
        }

        //Erzeuge den Ausgabe-String für die berechnete Querschnittsfläche
        public static string GetAsString()
        {
            string AserfText = "";
            if (MdGU)
            {
                AserfText = "Aserf = " + double.NaN + " cm²";
            }
            else
            {
                AserfText = "Aserf = " + Aserf + " cm²";
            }
            return AserfText;
        }

        //Erzeuge den Ausgabe-String für die ermittelten Bewehrungen
        public static string GetBewString()
        {
            string BewText = "";
            if (MdGU)
            {
                BewText = "MdGrenz wurde überschritten!";
            }
            else
            {
                BewText = "Bewehrungsauswahl:\n";
                if (Bewehrungen)
                {
                    BewText += Bewehrung[0][1] + "x " + Bewehrung[0][0] + "mm - Asvor = " +
                        GetAs(Bewehrung[0][1], Bewehrung[0][0]) + " cm²\n";
                    BewText += Bewehrung[1][1] + "x " + Bewehrung[1][0] + "mm - Asvor = " +
                        GetAs(Bewehrung[1][1], Bewehrung[1][0]) + " cm²\n";
                    BewText += Bewehrung[2][1] + "x " + Bewehrung[2][0] + "mm - Asvor = " +
                        GetAs(Bewehrung[2][1], Bewehrung[2][0]) + " cm²\n";
                }
                else
                {
                    BewText += "Keine gültigen Bewehrungen gefunden!";
                }
            }
            return BewText;
        }
    }
}


using System;
using System.Collections.Generic;

namespace TyCoDim_Library
{
    public static class Calc
    {
        //Berechnungsmodus
        public static string Mode = "Träger";

        //Konstanten
        public const double YC = 1.5; //Sicherheitsbeiwert Beton
        public const double YS = 1.15; //Sicherheitsbeiwert Stahl
        public const int DsT = 26; //Durchmesser Bewehrung Träger = 26mm (mm)
        public const int DsP = 16; //Durchmesser Bewehrung Platte = 16mm (mm)
        public const int DBue = 8; //Durchmesser Bügel (mm)

        //Werte aus der Tabelle
        //Beton
        private static int FCK = 0; //Druckfestigkeit Zylinder/Würfel
        private static double Lamda = 0;
        private static double Kappa = 0;
        private static double Chi = 0;
        //Stahl
        private static int FYK = 0; //Streckgrenze Stahl
        private static double MdG = 0;
        //Größtkorn
        private static int Gk = 0; //Größtkorn (mm)

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
        public static int[][] Bewehrung = new int[3][];
        public static bool Bewehrungen = false;
        public static bool MdGU = false;

        static Calc()
        {
            Bewehrung[0] = new int[2];
            Bewehrung[1] = new int[2];
            Bewehrung[2] = new int[2];
        }

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
        
        public static void GetBeton(string Auswahl)
        {
            switch (Auswahl)
            {
                case "C12/15":  FCK = 12; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C16/20":  FCK = 16; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C20/25":  FCK = 20; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C25/30":  FCK = 25; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C30/37":  FCK = 30; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C35/45":  FCK = 35; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C40/50":  FCK = 40; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C45/55":  FCK = 45; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C50/60":  FCK = 50; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
                case "C55/67":  FCK = 55; Kappa = 0.7442; Lamda = 0.3926; Chi = 2.1103; break;
                case "C60/75":  FCK = 60; Kappa = 0.6936; Lamda = 0.3768; Chi = 2.1729; break;
                case "C70/85":  FCK = 70; Kappa = 0.6268; Lamda = 0.3599; Chi = 2.2964; break;
                case "C80/95":  FCK = 80; Kappa = 0.5978; Lamda = 0.3547; Chi = 2.3732; break;
                case "C90/105": FCK = 90; Kappa = 0.5833; Lamda = 0.3529; Chi = 2.4204; break;
                default:        FCK = 12; Kappa = 0.8095; Lamda = 0.4160; Chi = 2.0554; break;
            }
        }

        public static void GetStahl(string Auswahl)
        {
            switch (Auswahl)
            {
                case "S420": FYK = 420; MdG = 0.387; break;
                case "S500": FYK = 500; MdG = 0.371; break;
                case "S550": FYK = 550; MdG = 0.362; break;
                case "S600": FYK = 600; MdG = 0.353; break;
                default:     FYK = 420; MdG = 0.387; break;
            }
        }

        public static void GetGk(string Auswahl)
        {
            switch (Auswahl)
            {
                case "16mm": Gk = 16; break;
                case "22mm": Gk = 22; break;
                case "32mm": Gk = 32; break;
                default:     Gk = 16; break;
            }
        }

        public static void Calculate()
        {
            switch(Mode)
            {
                case "Träger": CalculateT(); break;
                case "Platte": CalculateP(); break;
                default:       CalculateT(); break;
            }
        }

        private static bool CheckDistance(int st, int dS)
        {
            double e = 0;
            try
            {
                e = (100 * BT - 2 * Cnom - 0.24 * DBue - 0.12 * dS * st) / (st - 1);
            }
            catch
            {

            }
            if ((e >= 2) && (e >= 0.1 * dS) && (e >= 0.1 * Gk))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void CalculateT()
        {
            try
            {
                Bewehrung[0][0] = 0; Bewehrung[0][1] = 0;
                Bewehrung[1][0] = 0; Bewehrung[1][1] = 0;
                Bewehrung[2][0] = 0; Bewehrung[2][1] = 0;

                FCD = FCK / YC * 1000;
                FYD = FYK / YS * 0.1;

                DT = HT - Cnom * 0.01 - (0.0012 * DsT) / 2 - 0.0012 * DBue;
                D1 = Cnom + 0.012 * DBue + (0.012 * DsT) / 2;
                Md = MEd / (BT * DT * DT * FCD);
                Xi = 1.202 * (1 - Math.Sqrt(1 - 4 * (Lamda / Kappa) * Md));
                Zeta = 0.5 * (1 + Math.Sqrt(1 - 4 * (Lamda / Kappa) * Md));
                Aserf = Math.Round(MEd / (Zeta * DT * FYD), 2);
                if (Md > MdG)
                {
                    MdGU = true;
                    Bewehrungen = false;
                    return;
                }
                MdGU = false;
                if (Aserf <= 0)
                {
                    Bewehrungen = false;
                    return;
                }
                for (int d = 0, stk = 1, b = 0; b < 3;)
                {
                    int dS = AsIndexToCrossSection(d);
                    if (GetAs(stk, dS) >= Aserf)
                    {
                        if (CheckDistance(stk, dS))
                        {
                            Bewehrung[b][0] = dS;
                            Bewehrung[b][1] = stk;
                            b++;
                            d++;
                            stk = 1;
                        }
                        else
                        {
                            d++;
                            stk = 1;
                        }
                    }
                    else
                    {
                        if (stk >= 10)
                        {
                            if (d >= 10)
                            {
                                Bewehrungen = false;
                                return;
                            }
                            d++;
                            stk = 1;
                        }
                        else
                        {
                            stk++;
                        }
                    }
                }
                Bewehrungen = true;
            }
            catch
            {
            }
        }
        private static void CalculateP()
        {
            try
            {

            }
            catch
            {

            }
        }

        private static double GetAs(int stk, int dS)
        {
            return Math.Round(Math.Pow(dS / 2, 2) * Math.PI * stk * 0.01, 2);
        }
    }
}

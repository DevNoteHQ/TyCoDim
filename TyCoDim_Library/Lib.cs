
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

        //Stahlquerschnitte
        private static double[][] As = new double[11][];

        static Calc()
        {
            As[0]  = new double[] {  0.503,  1.01,  1.51,  2.01,  2.51,   3.02,   3.52,   4.02,   4.52,   5.03 };
            As[1]  = new double[] {  0.785,  1.57,  2.36,  3.14,  3.93,   4.71,   5.50,   6.28,   7.07,   7.85 };
            As[2]  = new double[] {  1.130,  2.26,  3.39,  4.52,  5.65,   6.79,   7.92,   9.05,  10.18,  11.31 };
            As[3]  = new double[] {  1.540,  3.08,  4.62,  6.16,  7.70,   9.24,  10.78,  12.32,  13.85,  15.39 };
            As[4]  = new double[] {  2.010,  4.02,  6.03,  8.04, 10.05,  12.06,  14.07,  16.08,  18.10,  20.11 };
            As[5]  = new double[] {  3.140,  6.28,  9.42, 12.57, 15.71,  18.85,  21.99,  25.13,  28.27,  31.42 };
            As[6]  = new double[] {  5.310, 10.62, 15.93, 21.24, 26.55,  31.86,  37.17,  42.47,  47.78,  53.09 };
            As[7]  = new double[] {  7.070, 14.14, 21.21, 28.27, 35.34,  42.41,  49.48,  56.55,  63.62,  70.69 };
            As[8]  = new double[] { 10.180, 20.36, 30.54, 40.72, 50.90,  61.08,  71.26,  81.44,  91.62, 101.80 };
            As[9]  = new double[] { 12.570, 25.14, 37.71, 50.28, 62.85,  75.42,  87.99, 100.56, 113.13, 125.70 };
            As[10] = new double[] { 19.630, 39.26, 58.89, 78.52, 98.15, 117.78, 137.41, 157.04, 176.67, 196.30 };
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
            e = (100 * BT - 2 * Cnom - 0.24 * DBue - 0.12 * dS * st) / (st - 1);
            if ((e >= 2) && (e >= dS) && (e >= 0.1 * Gk))
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
                FCD = FCK / YC * 1000;
                FYD = FYK / YS * 0.1;

                DT = HT - Cnom * 0.01 - (0.0012 * DsT) / 2 - 0.0012 * DBue;
                D1 = Cnom + 0.012 * DBue + (0.012 * DsT) / 2;
                Md = MEd / (BT * DT * DT * FCD);
                if(Md > MdG)
                {
                    //Fehlermeldung ausgeben!
                    return;
                }
                Xi = 1.202 * (1 - Math.Sqrt(1 - 4 * (Lamda / Kappa) * Md));
                Zeta = 0.5 * (1 + Math.Sqrt(1 - 4 * (Lamda / Kappa) * Md));
                Aserf = Math.Round(MEd / (Zeta * DT * FYD), 4);

                int[][] Bemaßung = new int[3][];
                Bemaßung[0] = new int[2];
                Bemaßung[0] = new int[2];
                Bemaßung[0] = new int[2];

                for (int d = 0, st = 0, b = 0; b < 3; st++)
                {
                    if(As[d][st] >= Aserf)
                    {
                        int dS = AsIndexToCrossSection(d);
                        if (CheckDistance(st + 1,dS))
                        {
                            Bemaßung[b][0] = dS;
                            Bemaßung[b][1] = st + 1;
                            b++;
                            d++;
                            st = 0;
                        }
                    }
                    else
                    {
                        if (st > 9)
                        {
                            if (d > 10)
                            {
                                //Keine bzw. zu wenige Querschnitt gefunden
                                return;
                            }
                            d++;
                            st = 0;
                        }
                    }
                }
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
    }
}

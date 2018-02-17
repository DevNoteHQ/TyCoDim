
using System;
using System.Collections.Generic;

namespace TyCoDim_Library
{
    public static class Calc
    {
        //Berechnungsmodus
        public static string sMode = "Träger";

        //Konstanten
        public const double dYC = 1.5; //Sicherheitsbeiwert Beton
        public const double dYS = 1.15; //Sicherheitsbeiwert Stahl
        public const int dDsT = 26; //Durchmesser Bewehrung Träger = 26mm
        public const int dDsP = 16; //Durchmesser Bewehrung Platte = 16mm
        public const int dDBue = 8; //Durchmesser Bügel

        //Werte aus der Tabelle
        //Beton
        private static int iFCK = 0; //Druckfestigkeit Zylinder/Würfel
        private static double dLamda = 0;
        private static double dKappa = 0;
        private static double dChi = 0;
        //Stahl
        private static int iFYK = 0; //Streckgrenze Stahl
        private static double dMdG = 0;

        //Eingebbare Werte
        public static double dBT = 0; //Breite des Trägers
        public static double dHT = 0; //Höhe des Trägers
        public static double dMEd = 0; //Bemessungsmoment
        public static double dCnom = 0; //Betondeckung

        //Berechnungswerte
        public static double dFCD = 0;
        public static double dFYD = 0;

        public static double dDT = 0;
        public static double dD1 = 0;
        public static double dMd = 0; //M = μ
        public static double dXi = 0;
        public static double dZeta = 0;
        public static double dAserf = 0; //Erforderliche Fläche

        //Stahlquerschnitte
        private static double[][] dAs = new double[11][];

        static Calc()
        {
            dAs[0]  = new double[] {  0.503,  1.01,  1.51,  2.01,  2.51,   3.02,   3.52,   4.02,   4.52,   5.03 };
            dAs[1]  = new double[] {  0.785,  1.57,  2.36,  3.14,  3.93,   4.71,   5.50,   6.28,   7.07,   7.85 };
            dAs[2]  = new double[] {  1.130,  2.26,  3.39,  4.52,  5.65,   6.79,   7.92,   9.05,  10.18,  11.31 };
            dAs[3]  = new double[] {  1.540,  3.08,  4.62,  6.16,  7.70,   9.24,  10.78,  12.32,  13.85,  15.39 };
            dAs[4]  = new double[] {  2.010,  4.02,  6.03,  8.04, 10.05,  12.06,  14.07,  16.08,  18.10,  20.11 };
            dAs[5]  = new double[] {  3.140,  6.28,  9.42, 12.57, 15.71,  18.85,  21.99,  25.13,  28.27,  31.42 };
            dAs[6]  = new double[] {  5.310, 10.62, 15.93, 21.24, 26.55,  31.86,  37.17,  42.47,  47.78,  53.09 };
            dAs[7]  = new double[] {  7.070, 14.14, 21.21, 28.27, 35.34,  42.41,  49.48,  56.55,  63.62,  70.69 };
            dAs[8]  = new double[] { 10.180, 20.36, 30.54, 40.72, 50.90,  61.08,  71.26,  81.44,  91.62, 101.80 };
            dAs[9]  = new double[] { 12.570, 25.14, 37.71, 50.28, 62.85,  75.42,  87.99, 100.56, 113.13, 125.70 };
            dAs[10] = new double[] { 19.630, 39.26, 58.89, 78.52, 98.15, 117.78, 137.41, 157.04, 176.67, 196.30 };
        }

        private static int AsIndexToCrossSection(int iIndex)
        {
            int iQuerschnitt = 0;
            switch(iIndex)
            {
                case  0: iQuerschnitt =  8; break;
                case  1: iQuerschnitt = 10; break;
                case  2: iQuerschnitt = 12; break;
                case  3: iQuerschnitt = 14; break;
                case  4: iQuerschnitt = 16; break;
                case  5: iQuerschnitt = 20; break;
                case  6: iQuerschnitt = 26; break;
                case  7: iQuerschnitt = 30; break;
                case  8: iQuerschnitt = 36; break;
                case  9: iQuerschnitt = 40; break;
                case 10: iQuerschnitt = 50; break;
                default: iQuerschnitt =  8; break;
            }
            return iQuerschnitt;
        }
        
        public static void GetBeton(string sAuswahl)
        {
            switch (sAuswahl)
            {
                case "C12/15":  iFCK = 12; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C16/20":  iFCK = 16; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C20/25":  iFCK = 20; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C25/30":  iFCK = 25; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C30/37":  iFCK = 30; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C35/45":  iFCK = 35; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C40/50":  iFCK = 40; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C45/55":  iFCK = 45; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C50/60":  iFCK = 50; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
                case "C55/67":  iFCK = 55; dKappa = 0.7442; dLamda = 0.3926; dChi = 2.1103; break;
                case "C60/75":  iFCK = 60; dKappa = 0.6936; dLamda = 0.3768; dChi = 2.1729; break;
                case "C70/85":  iFCK = 70; dKappa = 0.6268; dLamda = 0.3599; dChi = 2.2964; break;
                case "C80/95":  iFCK = 80; dKappa = 0.5978; dLamda = 0.3547; dChi = 2.3732; break;
                case "C90/105": iFCK = 90; dKappa = 0.5833; dLamda = 0.3529; dChi = 2.4204; break;
                default:        iFCK = 12; dKappa = 0.8095; dLamda = 0.4160; dChi = 2.0554; break;
            }
        }

        public static void GetStahl(string sAuswahl)
        {
            switch (sAuswahl)
            {
                case "S420": iFYK = 420; dMdG = 0.387; break;
                case "S500": iFYK = 500; dMdG = 0.371; break;
                case "S550": iFYK = 550; dMdG = 0.362; break;
                case "S600": iFYK = 600; dMdG = 0.353; break;
                default: iFYK = 420; dMdG = 0.387; break;
            }
        }

        public static void Calculate()
        {
            switch(sMode)
            {
                case "Träger": CalculateT(); break;
                case "Platte": CalculateP(); break;
                default: CalculateT(); break;
            }
        }

        private static void CalculateT()
        {
            try
            {
                dFCD = iFCK / dYC * 1000;
                dFYD = iFYK / dYS * 0.1;

                dDT = dHT - dCnom * 0.01 - (0.0012 * dDsT) / 2 - 0.0012 * dDBue;
                dD1 = dCnom + 0.012 * dDBue + (0.012 * dDsT) / 2;
                dMd = dMEd / (dBT * dDT * dDT * dFCD);
                if(dMd > dMdG)
                {
                    //Fehlermeldung ausgeben!
                    return;
                }
                dXi = 1.202 * (1 - Math.Sqrt(1 - 4 * (dLamda / dKappa) * dMd));
                dZeta = 0.5 * (1 + Math.Sqrt(1 - 4 * (dLamda / dKappa) * dMd));
                dAserf = Math.Round(dMEd / (dZeta * dDT * dFYD), 4);
            }
            catch
            {

            }
        }
        private static void CalculateP()
        {
            try
            {
                dFCD = iFCK / dYC * 1000;
                dFYD = iFYK / dYS * 0.1;

                dDT = dHT - dCnom * 0.01 - 0.0012 * dDsP;
                dMd = dMEd / (dBT * dDT * dDT * dFCD);
                dZeta = 0.5 * (1 + Math.Sqrt(1 - 2.055 * dMd));
                dAserf = dMEd / (dZeta * dDT * dFYD);
            }
            catch
            {

            }
        }
    }
}

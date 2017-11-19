
using System;

namespace TyCoDim_Library
{
    public static partial class Calc
    {
        public const double dYC = 1.5;
        public const double dYS = 1.15;

        public static int iFCK = 0;
        public static int iFYK = 0;

        public static double dPhiBew = 0;
        public static double dBTräger = 0;
        public static double dHTräger = 0;
        public static double dMEd = 0;
        public static double dCnom = 0;

        public static double dFCD = 0;
        public static double dFYD = 0;

        public static double dDTräger = 0;
        public static double dMd = 0;
        public static double dZ = 0;
        public static double dAserf = 0;

        public static void Calculate()
        {
            try
            {
                dFCD = iFCK / dYC * 1000;
                dFYD = iFYK / dYS * 0.1;

                dDTräger = dHTräger - dCnom * 0.01 - 0.0012 * dPhiBew;
                dMd = dMEd / (dBTräger * dDTräger * dDTräger * dFCD);
                dZ = 0.5 * (1 + Math.Sqrt(1 - 2.055 * dMd));
                dAserf = dMEd / (dZ * dDTräger * dFYD);
            }
            catch
            {

            }
        }
    }
}

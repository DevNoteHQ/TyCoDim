
namespace TSbb_Library
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

        public static double dDTräger;
        public static double dMd;
        public static double dZeichen;
        public static double dAserf;

        public static void Calculate()
        {
            dFCD = iFCK / dYC;
            dFYD = iFYK / dYS;
            //Sinlose Berechnungen bzw. Platzhalter:
            dDTräger = dFCD / dFYD; //dDTräger
            dMd = dFCD + dFYD; //dMd
            dZeichen = dFCD * dFYD; //dZeichen
            dAserf = dFCD - dFYD; //dAserf
        }
    }
}

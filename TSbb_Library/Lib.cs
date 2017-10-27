
namespace TSbb_Library
{
    public static partial class Calc
    {
        public static double[] Calculate(double dFCD, double dFYD)
        {
            double[] dErgebnisse = new double[4];
            //Sinlose Berechnungen bzw. Platzhalter:
            dErgebnisse[0] = dFCD / dFYD; //dDTräger
            dErgebnisse[1] = dFCD + dFYD; //dMd
            dErgebnisse[2] = dFCD * dFYD; //dZeichen
            dErgebnisse[3] = dFCD - dFYD; //dAserf
            return dErgebnisse;
        }
    }
}

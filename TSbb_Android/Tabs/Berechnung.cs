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
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using TSbb_Library;

namespace TSbb_Android.Tabs
{
    class Berechnung : Fragment
    {
        TextView BerechnungNr1;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Berechnung, container, false);
            BerechnungNr1 = rootView.FindViewById<TextView>(Resource.Id.Ergebnisse1);
            UpdateBerechnung();
            return rootView;
        }

        public void UpdateBerechnung()
        {
            BerechnungNr1.Text =
                "fcd = fck / yc\nfcd = " + Convert.ToString(Calc.dFCD) + " kN/m^2\n\n" +
                "fyd = fyd / ys\nfyd = " + Convert.ToString(Calc.dFYD) + " kN/cm^2\n\n" +
                "dTräger = hTräger - cnom - 1,2 * ΦBew\ndTräger = " + Convert.ToString(Calc.dDTräger) + " m\n\n" +
                "µd = MEd / (bTräger * dTräger ^ 2 * fcd)\nµd = " + Convert.ToString(Calc.dMd) + "\n\n" +
                "ζ = 0.5 * (1 + sqrt(1 - 2,055 * µd))\nζ = " + Convert.ToString(Calc.dZ) + "\n\n" +
                "Aserf = MEd / (s * dTräger * fyd)\nAserf = " + Convert.ToString(Calc.dAserf) + " cm^2";
        }
    }
}
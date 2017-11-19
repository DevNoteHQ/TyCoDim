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
using TyCoDim_Library;

namespace TyCoDim_Android.Tabs
{
    class Graphic : Fragment
    {
        TextView GraphicNr1;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Calculations, container, false);
            GraphicNr1 = rootView.FindViewById<TextView>(Resource.Id.Ergebnisse1);
            GraphicNr1.Text = "Grafik Platzhalter";
            return rootView;
        }
    }
}
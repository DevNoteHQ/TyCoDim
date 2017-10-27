using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using TSbb_Library;

namespace TSbb_Android
{
    [Activity(Label = "TSbb", MainLauncher = true)]
    public class MainActivity : Activity
    {
        const double dYC = 1.5;
        const double dYS = 1.15;

        int iFCK = 0;
        int iFYK = 0;

        double dFCD = 0;
        double dFYD = 0;

        double[] dErgebnisse = new double[4];

        Spinner BetonSpinner;
        Spinner StahlSpinner;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            BetonSpinner = FindViewById<Spinner>(Resource.Id.BetonSpinner);
            StahlSpinner = FindViewById<Spinner>(Resource.Id.StahlSpinner);


            BetonSpinner.ItemSelected += BetonSpinner_ItemSelected;
            StahlSpinner.ItemSelected += StahlSpinner_ItemSelected;

            var BetonSpinnerAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.BetonSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem);
            BetonSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var StahlSpinnerAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.StahlSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem);
            StahlSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            BetonSpinner.Adapter = BetonSpinnerAdapter;
            StahlSpinner.Adapter = StahlSpinnerAdapter;
        }

        private void BetonSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (BetonSpinner.SelectedItemPosition)
            {
                case 0:     iFCK = 12; break;
                case 1:     iFCK = 16; break;
                case 2:     iFCK = 25; break;
                case 3:     iFCK = 30; break;
                case 4:     iFCK = 35; break;
                case 5:     iFCK = 40; break;
                case 6:     iFCK = 45; break;
                default:    iFCK = 12; break;
            }
            dFCD = iFCK / dYC;
            dErgebnisse = Calc.Calculate(dFCD, dFYD);
        }

        private void StahlSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (StahlSpinner.SelectedItemPosition)
            {
                case 0:     iFYK = 500; break;
                case 1:     iFYK = 550; break;
                default:    iFYK = 500; break;
            }
            dFYD = iFYK / dYS;
            dErgebnisse = Calc.Calculate(dFCD, dFYD);
        }
    }
}


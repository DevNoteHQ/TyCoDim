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
    class Eingabe : Fragment
    {
        Spinner BetonSpinner;
        Spinner StahlSpinner;

        EditText PhiBew;
        EditText bTräger;
        EditText hTräger;
        EditText MEd;
        EditText cnom;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Eingabe, container, false);

            BetonSpinner = rootView.FindViewById<Spinner>(Resource.Id.BetonSpinner);
            StahlSpinner = rootView.FindViewById<Spinner>(Resource.Id.StahlSpinner);

            PhiBew = rootView.FindViewById<EditText>(Resource.Id.PhiBew);
            bTräger = rootView.FindViewById<EditText>(Resource.Id.bTräger);
            hTräger = rootView.FindViewById<EditText>(Resource.Id.hTräger);
            MEd = rootView.FindViewById<EditText>(Resource.Id.MEd);
            cnom = rootView.FindViewById<EditText>(Resource.Id.cnom);

            BetonSpinner.ItemSelected += BetonSpinner_ItemSelected;
            StahlSpinner.ItemSelected += StahlSpinner_ItemSelected;

            PhiBew.TextChanged += PhiBew_TextChanged;
            bTräger.TextChanged += BTräger_TextChanged;
            hTräger.TextChanged += HTräger_TextChanged;
            MEd.TextChanged += MEd_TextChanged;
            cnom.TextChanged += Cnom_TextChanged;

            var BetonSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.BetonSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem);
            BetonSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var StahlSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.StahlSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem);
            StahlSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            BetonSpinner.Adapter = BetonSpinnerAdapter;
            StahlSpinner.Adapter = StahlSpinnerAdapter;

            return rootView;
        }

        private void Cnom_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.dCnom = Convert.ToDouble(cnom.Text);
                Calc.Calculate();
            }
            catch
            {

            }
        }

        private void MEd_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.dMEd = Convert.ToDouble(MEd.Text);
                Calc.Calculate();
            }
            catch
            {

            }
        }

        private void HTräger_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.dHTräger = Convert.ToDouble(hTräger.Text);
                Calc.Calculate();
            }
            catch
            {

            }
        }

        private void BTräger_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.dBTräger = Convert.ToDouble(bTräger.Text);
                Calc.Calculate();
            }
            catch
            {

            }
        }

        private void PhiBew_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.dPhiBew = Convert.ToDouble(PhiBew.Text);
                Calc.Calculate();
            }
            catch
            {

            }
        }

        private void BetonSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (BetonSpinner.SelectedItemPosition)
            {
                case 0: Calc.iFCK = 12; break;
                case 1: Calc.iFCK = 16; break;
                case 2: Calc.iFCK = 25; break;
                case 3: Calc.iFCK = 30; break;
                case 4: Calc.iFCK = 35; break;
                case 5: Calc.iFCK = 40; break;
                case 6: Calc.iFCK = 45; break;
                default: Calc.iFCK = 12; break;
            }
            Calc.Calculate();
        }

        private void StahlSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (StahlSpinner.SelectedItemPosition)
            {
                case 0: Calc.iFYK = 500; break;
                case 1: Calc.iFYK = 550; break;
                default: Calc.iFYK = 500; break;
            }
            Calc.Calculate();
        }
    }
}
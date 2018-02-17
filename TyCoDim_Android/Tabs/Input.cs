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
    class Input : Fragment
    {
        Spinner BetonSpinner;
        Spinner StahlSpinner;

        EditText bT;
        EditText hT;
        EditText MEd;
        EditText cnom;

        TextView TbT;
        TextView ThT;
        TextView TMEd;
        TextView Tcnom;

        TextView Aserf;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Input, container, false);

            BetonSpinner = rootView.FindViewById<Spinner>(Resource.Id.BetonSpinner);
            StahlSpinner = rootView.FindViewById<Spinner>(Resource.Id.StahlSpinner);

            bT = rootView.FindViewById<EditText>(Resource.Id.bT);
            hT = rootView.FindViewById<EditText>(Resource.Id.hT);
            MEd = rootView.FindViewById<EditText>(Resource.Id.MEd);
            cnom = rootView.FindViewById<EditText>(Resource.Id.cnom);

            TbT = rootView.FindViewById<TextView>(Resource.Id.TbT);
            ThT = rootView.FindViewById<TextView>(Resource.Id.ThT);
            TMEd = rootView.FindViewById<TextView>(Resource.Id.TMEd);
            Tcnom = rootView.FindViewById<TextView>(Resource.Id.Tcnom);

            Aserf = rootView.FindViewById<TextView>(Resource.Id.Aserf);

            TbT.Text = "bT = ";
            ThT.Text = "hT = ";
            TMEd.Text = "MEd = ";
            Tcnom.Text = "cnom = ";

            BetonSpinner.ItemSelected += BetonSpinner_ItemSelected;
            StahlSpinner.ItemSelected += StahlSpinner_ItemSelected;

            bT.TextChanged += BTräger_TextChanged;
            hT.TextChanged += HTräger_TextChanged;
            MEd.TextChanged += MEd_TextChanged;
            cnom.TextChanged += Cnom_TextChanged;

            var BetonSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.BetonSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem); //Resources->values->Strings.xml
            BetonSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var StahlSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.StahlSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem); //Resources->values->Strings.xml
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
                UpdateGUI();
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
                UpdateGUI();
            }
            catch
            {

            }
        }

        private void HTräger_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.dHT = Convert.ToDouble(hT.Text);
                Calc.Calculate();
                UpdateGUI();
            }
            catch
            {

            }
        }

        private void BTräger_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.dBT = Convert.ToDouble(bT.Text);
                Calc.Calculate();
                UpdateGUI();
            }
            catch
            {

            }
        }

        private void BetonSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Calc.GetBeton(BetonSpinner.SelectedItem.ToString());
            Calc.Calculate();
            UpdateGUI();
        }

        private void StahlSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Calc.GetStahl(StahlSpinner.SelectedItem.ToString());
            Calc.Calculate();
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            Aserf.Text = "Aserf = " + Calc.dAserf + " cm²";
        }
    }
}
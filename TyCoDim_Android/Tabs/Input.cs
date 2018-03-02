using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

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
        Spinner GkSpinner;

        EditText bT;
        EditText hT;
        EditText MEd;
        EditText cnom;

        TextView TbT;
        TextView ThT;
        TextView TMEd;
        TextView Tcnom;

        TextView Aserf;
        TextView Bew;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Input, container, false);

            BetonSpinner = rootView.FindViewById<Spinner>(Resource.Id.BetonSpinner);
            StahlSpinner = rootView.FindViewById<Spinner>(Resource.Id.StahlSpinner);
            GkSpinner = rootView.FindViewById<Spinner>(Resource.Id.GkSpinner);

            bT = rootView.FindViewById<EditText>(Resource.Id.bT);
            hT = rootView.FindViewById<EditText>(Resource.Id.hT);
            MEd = rootView.FindViewById<EditText>(Resource.Id.MEd);
            cnom = rootView.FindViewById<EditText>(Resource.Id.cnom);

            TbT = rootView.FindViewById<TextView>(Resource.Id.TbT);
            ThT = rootView.FindViewById<TextView>(Resource.Id.ThT);
            TMEd = rootView.FindViewById<TextView>(Resource.Id.TMEd);
            Tcnom = rootView.FindViewById<TextView>(Resource.Id.Tcnom);

            Aserf = rootView.FindViewById<TextView>(Resource.Id.Aserf);
            Bew = rootView.FindViewById<TextView>(Resource.Id.Bew);

            TbT.Text = "bT = ";
            ThT.Text = "hT = ";
            TMEd.Text = "MEd = ";
            Tcnom.Text = "cnom = ";

            BetonSpinner.ItemSelected += BetonSpinner_ItemSelected;
            StahlSpinner.ItemSelected += StahlSpinner_ItemSelected;
            GkSpinner.ItemSelected += GkSpinner_ItemSelected;

            bT.TextChanged += BTräger_TextChanged;
            hT.TextChanged += HTräger_TextChanged;
            MEd.TextChanged += MEd_TextChanged;
            cnom.TextChanged += Cnom_TextChanged;

            var BetonSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.BetonSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem); //Resources->values->Strings.xml
            BetonSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var StahlSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.StahlSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem); //Resources->values->Strings.xml
            StahlSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var GkSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.GkSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem); //Resources->values->Strings.xml
            GkSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            BetonSpinner.Adapter = BetonSpinnerAdapter;
            StahlSpinner.Adapter = StahlSpinnerAdapter;
            GkSpinner.Adapter = GkSpinnerAdapter;

            BetonSpinner.SetSelection(3);
            StahlSpinner.SetSelection(2);
            GkSpinner.SetSelection(1);

            return rootView;
        }

        private void Cnom_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                Calc.Cnom = double.Parse(cnom.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
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
                Calc.MEd = double.Parse(MEd.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
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
                Calc.HT = double.Parse(hT.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
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
                Calc.BT = double.Parse(bT.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
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

        private void GkSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Calc.GetGk(GkSpinner.SelectedItem.ToString());
            Calc.Calculate();
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            Aserf.Text = "Aserf = " + Calc.Aserf + " cm²";
            Bew.Text = "Bewehrungsauswahl:\n";
            Bew.Text += Calc.Bewehrung[0][1] + "x " + Calc.Bewehrung[0][0] + "mm - Asvor = " + 
                Math.Round(Math.Pow(Calc.Bewehrung[0][0] / 2, 2) * Math.PI * Calc.Bewehrung[0][1] * 0.01, 2) + " cm²\n";
            Bew.Text += Calc.Bewehrung[1][1] + "x " + Calc.Bewehrung[1][0] + "mm - Asvor = " +
                Math.Round(Math.Pow(Calc.Bewehrung[1][0] / 2, 2) * Math.PI * Calc.Bewehrung[1][1] * 0.01, 2) + " cm²\n";
            Bew.Text += Calc.Bewehrung[2][1] + "x " + Calc.Bewehrung[2][0] + "mm - Asvor = " +
                Math.Round(Math.Pow(Calc.Bewehrung[2][0] / 2, 2) * Math.PI * Calc.Bewehrung[2][1] * 0.01, 2) + " cm²\n";
        }
    }
}
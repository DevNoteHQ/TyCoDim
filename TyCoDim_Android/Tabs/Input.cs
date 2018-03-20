
/*
 * 
 * This file is part of TyCoDim.
 * TyCoDim is a program to calculate reinforced concrete beams
 * 
 * Copyright (C) 2018 Mathias Schöpf <schoepf.mathias@gmail.com>
 *
 * TyCoDim is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * TyCoDim is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with TyCoDim. If not, see <http://www.gnu.org/licenses/>.
 * 
 */


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

using TyCoDim_Library;

namespace TyCoDim_Android.Tabs
{
    class Input : Fragment
    {
        //Deklarationen für die Auswahlfelder
        Spinner BetonSpinner;
        Spinner StahlSpinner;
        Spinner GkSpinner;

        //Deklarationen für die Eingabefelder
        EditText bT;
        EditText hT;
        EditText MEd;
        EditText cnom;

        //Deklarationen für die Ausgabefelder
        TextView Aserf;
        TextView Bew;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //Setzt die Sicht des Fragments auf Input.axml
            View rootView = inflater.Inflate(Resource.Layout.Input, container, false);

            //Den oben deklarierten Feldern werden ihre Elemente in der GUI zugeordnet
            BetonSpinner = rootView.FindViewById<Spinner>(Resource.Id.BetonSpinner);
            StahlSpinner = rootView.FindViewById<Spinner>(Resource.Id.StahlSpinner);
            GkSpinner = rootView.FindViewById<Spinner>(Resource.Id.GkSpinner);

            bT = rootView.FindViewById<EditText>(Resource.Id.bT);
            hT = rootView.FindViewById<EditText>(Resource.Id.hT);
            MEd = rootView.FindViewById<EditText>(Resource.Id.MEd);
            cnom = rootView.FindViewById<EditText>(Resource.Id.cnom);

            Aserf = rootView.FindViewById<TextView>(Resource.Id.Aserf);
            Bew = rootView.FindViewById<TextView>(Resource.Id.Bew);

            //Jedes Auswahlfeld benötigt noch Elemente zur Auswahl
            //Diese sind in Resources->values->Strings.xml definiert
            var BetonSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.BetonSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem);
            BetonSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var StahlSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.StahlSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem);
            StahlSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var GkSpinnerAdapter = ArrayAdapter.CreateFromResource(rootView.Context, Resource.Array.GkSpinner_Elements, Android.Resource.Layout.SimpleSpinnerItem);
            GkSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            //Die extrahierten Listen werden den Auswahlfeldern zugeteilt
            BetonSpinner.Adapter = BetonSpinnerAdapter;
            StahlSpinner.Adapter = StahlSpinnerAdapter;
            GkSpinner.Adapter = GkSpinnerAdapter;

            //Das Default-Element wird ausgewählt
            BetonSpinner.SetSelection(3);
            StahlSpinner.SetSelection(2);
            GkSpinner.SetSelection(1);

            //Jedem Auswahlfeld wird ein ItemSelected-Event zugeordnet
            BetonSpinner.ItemSelected += BetonSpinner_ItemSelected;
            StahlSpinner.ItemSelected += StahlSpinner_ItemSelected;
            GkSpinner.ItemSelected += GkSpinner_ItemSelected;

            //Jedem Eingabefeld wird ein TextChanged-Event zugeordnet
            bT.TextChanged += BTräger_TextChanged;
            hT.TextChanged += HTräger_TextChanged;
            MEd.TextChanged += MEd_TextChanged;
            cnom.TextChanged += Cnom_TextChanged;

            return rootView;
        }

        private void Cnom_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                //Cnom wird aus der Eingabe ermittelt, formatiert und an die Library weitergegeben
                Calc.Cnom = double.Parse(cnom.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                Update();
            }
            catch { }
        }

        private void MEd_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                //MEd wird aus der Eingabe ermittelt, formatiert und an die Library weitergegeben
                Calc.MEd = double.Parse(MEd.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                Update();
            }
            catch { }
        }

        private void HTräger_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                //HT wird aus der Eingabe ermittelt, formatiert und an die Library weitergegeben
                Calc.HT = double.Parse(hT.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                Update();
            }
            catch { }
        }

        private void BTräger_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                //BT wird aus der Eingabe ermittelt, formatiert und an die Library weitergegeben
                Calc.BT = double.Parse(bT.Text.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                Update();
            }
            catch { }
        }

        private void BetonSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Übergibt das ausgewählte Element der GetBeton-Funktion
            Calc.GetBeton(BetonSpinner.SelectedItem.ToString());
            Update();
        }

        private void StahlSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Übergibt das ausgewählte Element der GetStahl-Funktion
            Calc.GetStahl(StahlSpinner.SelectedItem.ToString());
            Update();
        }

        private void GkSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Übergibt das ausgewählte Element der GetGk-Funktion
            Calc.GetGk(GkSpinner.SelectedItem.ToString());
            Update();
        }

        //Erneute Berechnung, Update der Ausgabe
        private void Update()
        {
            Calc.CalculateT();
            Aserf.Text = Calc.GetAsString();
            Bew.Text = Calc.GetBewString();
        }
    }
}
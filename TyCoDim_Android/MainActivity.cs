
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

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V4.App;
using ActionBar = Android.App.ActionBar;

using TyCoDim_Android.Tabs;

namespace TyCoDim_Android
{
    //ScreenOrientation = ScreenOrientation.Portrait: Der Ladebildschirm soll nicht kippbar sein
    [Activity(Label = "TyCoDim", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FragmentActivity
    {
        //Deklaration des ViewPagers und des TabLayouts
        //Mit dem ViewPager können Fragmente dargestellt werden, die per Swipe geändert werden können
        //Das TabLayout zeigt an, auf welcher Seite bzw. auf welchem Fragment man sich gerade befindet
        ViewPager viewPager;
        TabLayout tabLayout;

        //Deklaration der beiden Fragemente, die dann mit dem ViewPager angezeigt werden
        Input Input = new Input();
        Graphic Graphic = new Graphic();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); //Die ursprüngliche Funktion von OnCreate soll beibehalten werden

            SetContentView(Resource.Layout.Main); //Setzt die aktuelle Sicht auf Main.axml

            //Erstellt eine neue Toolbar aus der Toolbar auf der GUI
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar); 
            //Ändert die Farbe der Toolbar zu Weiß
            toolbar.SetTitleTextColor(Android.Graphics.Color.White);
            //Aktiviert die Toolbar, ab da Toolbar = Actionbar
            SetActionBar(toolbar);

            //Setzt den Titel der Actionbar
            ActionBar.Title = "TyCoDim";

            //Den oben deklarierten Feldern werden ihre Elemente in der GUI zugeordnet
            viewPager = (ViewPager)FindViewById(Resource.Id.pager);
            tabLayout = (TabLayout)FindViewById(Resource.Id.tabs);
            //Aktiviert den ViewPager
            SetupViewPager(viewPager);
            //Aktiviert das TabLayout mit dem ViewPager
            tabLayout.SetupWithViewPager(viewPager, true);

            //Dem ViewPager wird ein PageSelected-Event zugewiesen
            viewPager.PageSelected += ViewPager_PageSelected;
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            //Blendet die Tastatur aus, sollte der User auf die Grafik-Seite wechseln
            if (e.Position == 1)
            {
                var im = ((InputMethodManager)GetSystemService(InputMethodService));
                if (CurrentFocus != null)
                {
                    im.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.None);
                }
            }
        }

        //Überschreibt das OnBackPressed-Event, damit der Ladebildschirm nicht wieder auftaucht.
        //Drückt man nun "Zurück", wird man nur auf die Eingabe-Seite weitergeleitet
        public override void OnBackPressed()
        {
            viewPager.SetCurrentItem(0, true);
        }

        //Der ViewPager benötigt noch einen PageAdapter, der mit dieser Funktion erstellt und zugewiesen wird
        private void SetupViewPager(ViewPager Pager)
        {
            Pager.OffscreenPageLimit = 2;

            PageAdapter adapter = new PageAdapter(SupportFragmentManager);
            adapter.AddFragment(Input, "");
            adapter.AddFragment(Graphic, "");

            Pager.Adapter = adapter;
        }
    }
}


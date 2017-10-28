using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using TSbb_Android.Tabs;
using Android.Support.V4.App;
using ActionBar = Android.App.ActionBar;

namespace TSbb_Android
{
    [Activity(Label = "TSbb", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        ViewPager viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);

            ActionBar.Title = "TSbb";

            viewPager = (ViewPager)FindViewById(Resource.Id.pager);
            SetupViewPager(viewPager);

            viewPager.PageSelected += ViewPager_PageSelected;
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            
        }

        private void SetupViewPager(ViewPager Pager)
        {
            Pager.OffscreenPageLimit = 2;

            PageAdapter adapter = new PageAdapter(SupportFragmentManager);
            adapter.AddFragment(new Eingabe(), "Eingabe");
            adapter.AddFragment(new Berechnung(), "Berechnung");

            Pager.Adapter = adapter;
        }
    }
}


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
using TyCoDim_Android.Tabs;
using Android.Support.V4.App;
using ActionBar = Android.App.ActionBar;

namespace TyCoDim_Android
{
    [Activity(Label = "TyCoDim", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        ViewPager viewPager;
        Input Input = new Input();
        Graphic Graphic = new Graphic();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);

            ActionBar.Title = "TyCoDim";

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
            adapter.AddFragment(Input, "Input");
            adapter.AddFragment(Graphic, "Graphic");

            Pager.Adapter = adapter;
        }
    }
}


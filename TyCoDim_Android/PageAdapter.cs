
using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace TyCoDim_Android
{
    class PageAdapter : FragmentPagerAdapter
    {
        private readonly List<Fragment> _fragments;
        private readonly List<string> _fragmentnames;

        public PageAdapter(FragmentManager fm) : base(fm)
        {
            _fragments = new List<Fragment>();
            _fragmentnames = new List<string>();
        }

        public override int Count
        {
            get { return _fragments.Count; }
        }
        public override Fragment GetItem(int position)
        {
            return _fragments[position];
        }

        public void AddFragment(Fragment fragment, string name)
        {
            if (fragment == null) return;
            _fragments.Add(fragment);
            _fragmentnames.Add(name);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_fragmentnames[position]);
        }
    }
}
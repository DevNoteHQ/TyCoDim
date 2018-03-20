
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


using System.Collections.Generic;

using Android.Support.V4.App;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

using Java.Lang;

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
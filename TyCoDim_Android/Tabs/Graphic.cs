
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

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

using TyCoDim_Library;

namespace TyCoDim_Android.Tabs
{
    class Graphic : Fragment
    {
        //Lediglich ein Platzhalter im Moment
        TextView GraphicNr1;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Graphic, container, false);
            GraphicNr1 = rootView.FindViewById<TextView>(Resource.Id.Graphic);
            GraphicNr1.Text = "Grafik Platzhalter\nWork in Progress";
            return rootView;
        }
    }
}
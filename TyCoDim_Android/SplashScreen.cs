
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
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace TyCoDim_Android
{
    //MainLauncher = true:  Beim Start der App wird diese Activity gestartet
    //NoHistory = true:     Der Ladebildschirm soll nicht mit der "Zurück"-Taste erreichbar sein
    //ScreenOrientation = ScreenOrientation.Portrait: Der Ladebildschirm soll nicht kippbar sein
    [Activity(Theme = "@style/MyTheme.Splash", Icon = "@drawable/icon", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); //Die ursprüngliche Funktion von OnCreate soll beibehalten werden
            SetContentView(Resource.Layout.SplashScreen); //Setzt die aktuelle Sicht auf SplashScreen.axml
        }

        //Damit auf dem Ladebildschirm keine unerwünschte Zustände entstehen,
        //wird das OnBackPressed() Event überschrieben, sodass diese nichts macht
        public override void OnBackPressed() { }

        //Startet die Hauptanwendung
        protected override void OnResume()
        {
            base.OnResume(); //Die ursprüngliche Funktion von OnResume soll beibehalten werden
            Task startupWork = new Task(() => { SimulateStartup(); }); //Erstellt einen neuen Task für die Methode SimulateStartup()
            startupWork.Start(); //Startet den neuen Task
        }

        //Verzögert den Start der Hauptanwendung um 3 Sekunden, damit man den Ladebildschirm noch sieht
        async void SimulateStartup()
        {
            await Task.Delay(3000); //3 Sekunden Delay
            StartActivity(new Intent(Application.Context, typeof(MainActivity))); //Startet die Hauptanwendung
        }
    }
}
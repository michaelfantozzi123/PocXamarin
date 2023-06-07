using System;
using Android.Content;
using Android.Provider;
using PocXamarin.Droid.Services;
using PocXamarin.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NfcSettingsOpener))]
namespace PocXamarin.Droid.Services
{
    public class NfcSettingsOpener : INfcSettingOpener
    {
        public void OpenNfcSettings()
        {
            var intent = new Intent(Settings.ActionNfcSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}


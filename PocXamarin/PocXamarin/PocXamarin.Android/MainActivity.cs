using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.NFC;
using Prism;
using Prism.Ioc;

namespace PocXamarin.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossNFC.Init(this);
            CrossNFC.Current.SetConfiguration(new NfcConfiguration
            {
                Messages = new UserDefinedMessages
                {
                    NFCSessionInvalidated = "Session invalidée",
                    NFCSessionInvalidatedButton = "OK",
                    NFCWritingNotSupported = "L'écriture des TAGs NFC n'est pas supporté sur cet appareil",
                    NFCDialogAlertMessage = "Approchez votre appareil du tag NFC",
                    NFCErrorRead = "Erreur de lecture. Veuillez rééssayer",
                    NFCErrorEmptyTag = "Ce tag est vide",
                    NFCErrorReadOnlyTag = "Ce tag n'est pas accessible en écriture",
                    NFCErrorCapacityTag = "La capacité de ce TAG est trop basse",
                    NFCErrorMissingTag = "Aucun tag trouvé",
                    NFCErrorMissingTagInfo = "Aucune information à écrire sur le tag",
                    NFCErrorNotSupportedTag = "Ce tag n'est pas supporté",
                    NFCErrorNotCompliantTag = "Ce tag n'est pas compatible NDEF",
                    NFCErrorWrite = "Aucune information à écrire sur le tag",
                    NFCSuccessRead = "Lecture réussie",
                    NFCSuccessWrite = "Ecriture réussie",
                    NFCSuccessClear = "Effaçage réussi"
                }
            });
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();

            // Plugin NFC: Restart NFC listening on resume (needed for Android 10+) 
            CrossNFC.OnResume();
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            // Plugin NFC: Tag Discovery Interception
            CrossNFC.OnNewIntent(intent);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}


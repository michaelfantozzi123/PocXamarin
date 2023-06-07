using System;
using Prism.Commands;
using Prism.Navigation;
using Plugin.NFC;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;
using PocXamarin.Services;
using Prism.Navigation.Xaml;
using NavigationParameters = Prism.Navigation.NavigationParameters;

namespace PocXamarin.ViewModels
{
    public class NFCScanPageViewModel : ViewModelBase
    {

        private bool _isScanning;

        public bool IsScanning
        {
            get { return _isScanning; }
            set { SetProperty(ref _isScanning, value); }
        }

        public DelegateCommand ScanNFCCommand { get; private set; }

        public NFCScanPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ScanNFCCommand = new DelegateCommand(ScanNFC);

            // Event raised when a ndef message is received.
            CrossNFC.Current.OnMessageReceived += Current_OnMessageReceivedAsync;
            // Event raised when a ndef message has been published.
            CrossNFC.Current.OnMessagePublished += Current_OnMessagePublished;
            // Event raised when a tag is discovered. Used for publishing.
            CrossNFC.Current.OnTagDiscovered += Current_OnTagDiscovered;
            // Event raised when NFC listener status changed
            CrossNFC.Current.OnTagListeningStatusChanged += Current_OnTagListeningStatusChanged;
            // Event raised when NFC state has changed.
            CrossNFC.Current.OnNfcStatusChanged += Current_OnNfcStatusChanged;
        }

        private async void ScanNFC()
        {

            if (IsScanning)
            {
                return;
            }

            if(CrossNFC.IsSupported)
            {
                if (!CrossNFC.Current.IsAvailable || !CrossNFC.Current.IsEnabled)
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("NFC Disabled", "NFC is not enabled. Would you like to enable it now?", "Yes", "No");

                    if (answer)
                    {
                        // The user agreed to enable NFC.
                        // On Android, you can use an Intent to open the NFC settings here.
                        DependencyService.Get<INfcSettingOpener>().OpenNfcSettings();
                        // On iOS, you can display a message telling the user to enable NFC manually.
                    }
                }
                else
                {
                    IsScanning = true;
                    CrossNFC.Current.StartListening();

                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("useModalNavigation", true);
                    await NavigationService.NavigateAsync(nameof(ScanningOverlayPage), navigationParameters);


                }
            }

        }

        private void Current_OniOSReadingSessionCancelled(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Current_OnNfcStatusChanged(bool isEnabled)
        {
            //throw new NotImplementedException();
        }

        private void Current_OnTagListeningStatusChanged(bool isListening)
        {
            //throw new NotImplementedException();
        }

        private void Current_OnTagDiscovered(ITagInfo tagInfo, bool format)
        {
            //throw new NotImplementedException();
        }

        private void Current_OnMessagePublished(ITagInfo tagInfo)
        {
            //throw new NotImplementedException();
        }

        private async void Current_OnMessageReceivedAsync(ITagInfo tagInfo)
        {
            if (tagInfo == null)
            {
                // Handle the case where tagInfo is null
                return;
            }

            // Prepare the tag info string
            var tagInfoString = new StringBuilder()
                .AppendLine($"Identifier: {BitConverter.ToString(tagInfo.Identifier)}")
                .AppendLine($"Serial Number: {tagInfo.SerialNumber}")
                .AppendLine($"Is Writable: {tagInfo.IsWritable}")
                .AppendLine($"Is Empty: {tagInfo.IsEmpty}")
                .AppendLine($"Is Supported: {tagInfo.IsSupported}")
                .AppendLine($"Capacity: {tagInfo.Capacity}");

            if (tagInfo.Records != null)
            {
                foreach (var record in tagInfo.Records)
                {
                    tagInfoString.AppendLine($"Record: {record.Message}");
                }
            }

            // Display an alert with the tag info
            if (Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("NFC Tag Detected", tagInfoString.ToString(), "OK");
            }

            CrossNFC.Current.StopListening();
            IsScanning = false;
            await NavigationService.GoBackAsync();
        }
    }

    // Laziness personified
    public class ScanningOverlayPage : ContentPage
    {
        public ScanningOverlayPage()
        {
            BackgroundColor = Color.FromRgba(0, 0, 0, 0.5); // semi-transparent background

            var activityIndicator = new ActivityIndicator
            {
                IsRunning = true,
                Color = Color.White,
            };

            var label = new Label
            {
                Text = "Scanning...",
                TextColor = Color.White,
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = { activityIndicator, label },
            };
        }
    }
}

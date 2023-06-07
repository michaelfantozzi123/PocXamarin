using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;

namespace PocXamarin.ViewModels
{
	public class BonusPageViewModel : ViewModelBase
	{
        private ICommand _locationCommand;
        public ICommand LocationCommand
        {
            get { return _locationCommand ?? (_locationCommand = new Command(GetLocation)); }
        }

        private string _lblResult;
        public string LblResult
        {
            get { return _lblResult; }
            set { SetProperty(ref _lblResult, value); }
        }

        public BonusPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
		}

		private async void GetLocation()
		{
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    LblResult = $"{location.Latitude} {location.Longitude}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
	}
}


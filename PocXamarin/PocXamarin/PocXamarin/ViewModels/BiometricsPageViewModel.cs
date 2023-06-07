using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PocXamarin.ViewModels
{
	public class BiometricsPageViewModel : ViewModelBase
	{
        public ICommand AuthenticateCommand { get; set; }

        private const string Default = "https://cdn-icons-png.flaticon.com/512/2313/2313181.png";
        private const string Success = "https://assets-global.website-files.com/61845f7929f5aa517ebab941/61ac7019cc8c4f2d62b8198c_Aratek%20TrustFinger%20SDK-icon.jpg";
        private string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public BiometricsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            AuthenticateCommand = new Command(async () => await Authenticate());

            Url = Default;
        }

		public async Task<bool> Authenticate()
		{
		    var isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(true);
		    
		    if (isFingerprintAvailable)
		    {
                var request = new AuthenticationRequestConfiguration("Biometrics Sign In", "Authenticate access to your account")
                {
                    AllowAlternativeAuthentication = true // if you want to allow PIN / password as fallback
                };

                var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                if (result.Authenticated)
                {
                    // Authenticated successfully, navigate to the next page
                    Url = Success;
                    return true;
                }

                // Not authenticated
                Url = Default;
                return false;
            }

            // Device doesn't support biometrics
            Url = Default;
            return false;
        }
    }
}


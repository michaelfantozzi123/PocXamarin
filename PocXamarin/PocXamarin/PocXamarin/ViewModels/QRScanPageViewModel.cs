using System;
using System.ComponentModel;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Xaml;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace PocXamarin.ViewModels
{
    public class QRScanPageViewModel : ViewModelBase
    {

        private ICommand scanCommand;
        public ICommand ScanCommand
        {
            get { return scanCommand ?? (scanCommand = new Command<Result>(Scan)); }
        }

        private string _lblResult;
        public string LblResult
        {
            get { return _lblResult; }
            set { SetProperty(ref _lblResult, value); }
        }

        public QRScanPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
            
        private async void Scan(Result res)
        {
            LblResult = res.Text;
        }
    }
}


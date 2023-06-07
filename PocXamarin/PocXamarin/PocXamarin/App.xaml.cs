using PocXamarin.ViewModels;
using PocXamarin.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace PocXamarin
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ScanningOverlayPage>();

            containerRegistry.RegisterForNavigation<BiometricsPage, BiometricsPageViewModel>();
            containerRegistry.RegisterForNavigation<BonusPage, BonusPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<NFCScanPage, NFCScanPageViewModel>();
            containerRegistry.RegisterForNavigation<PieChartPage, PieChartPageViewModel>();
            containerRegistry.RegisterForNavigation<QRScanPage, QRScanPageViewModel>();
        }
    }
}

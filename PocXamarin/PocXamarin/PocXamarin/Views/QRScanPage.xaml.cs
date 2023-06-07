using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PocXamarin.ViewModels
{	
	public partial class QRScanPage : ContentPage
	{	
		public QRScanPage ()
		{
			InitializeComponent ();
        }

        private void scannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                lblResult.Text = result.Text;
            });
        }
    }
}


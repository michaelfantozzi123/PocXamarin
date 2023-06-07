using System;

using Xamarin.Forms;

namespace PocXamarin.Views
{
	public class BonusPage : ContentPage
	{
		public BonusPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}



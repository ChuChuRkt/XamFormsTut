﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectXamarin.Models;
using TestProjectXamarin.Views.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProjectXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            Init();
		}

	    private void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            App.StartCheckIfInternet(lbl_NoInternet, this);
            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

	    private async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            if(user.CheckInformation())
            {
                ActivitySpinner.IsVisible = true;
                //var result = await App.RestService.Login(user);
                var result = new Token();
                await DisplayAlert("Login", "Login Success", "Ok");
                
                if(result != null)
                {
                    ActivitySpinner.IsVisible = false;
                    //App.UserDatabase.SaveUser(user);
                    //App.TokenDatabase.SaveToken(result);
                    if(Device.OS == TargetPlatform.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new MasterDetail());
                    }
                    //else if(Device.OS == TargetPlatform.iOS)
                    //{
                    //    await Navigation.PushModalAsync(new NavigationPage(new MasterDetail()));
                    //}
                }
            }
            else
            {
                await DisplayAlert("Login", "Login Failed", "Ok");
                ActivitySpinner.IsVisible = false;
            }
        }
    }
}
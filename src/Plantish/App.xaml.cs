using System;
using Plantish.ViewModels;
using Plantish.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantish
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            TheFramework.TheFramework.Init();
            var resource = Resources["ViewModelLocator"];
            if (resource is ViewModelLocator viewModelLocator)
            {
                viewModelLocator.Initialize();
            }

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

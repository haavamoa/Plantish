using System;
using Plantish.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantish
{
    public partial class App : Application
    {
        private AppShell m_appShell;

        public App()
        {
            InitializeComponent();

            MainPage = m_appShell =  new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            m_appShell.OnStart();
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

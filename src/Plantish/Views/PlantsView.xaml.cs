using System;
using Plantish.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantsView : ContentView
    {
        public PlantsView()
        {
            InitializeComponent();
        }

        public void OnStart()
        {
            ((PlantsViewModel)BindingContext).Initialize();
        }

        private void AddPlantButton_OnClicked(object sender, EventArgs e)
        {
            if(App.Current.MainPage is MainPage mainPage)
            {
                mainPage.ChangeContent<AddPlantView>();
            }
        }
    }
}
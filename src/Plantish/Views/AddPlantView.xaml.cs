using Plantish.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Plantish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlantView : ContentView
    {
        public AddPlantView()
        {
            InitializeComponent();
        }

        public void OnStart()
        {
            if(App.Current.MainPage is MainPage mainPage)
            {
                ((AddPlantViewModel)BindingContext).Initialize( () => mainPage.ChangeContent<PlantsView>());
            }
            
        }
    }
}
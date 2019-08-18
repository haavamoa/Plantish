using Plantish.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Plantish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlantView : ContentPage
    {
        public AddPlantView()
        {
            InitializeComponent();
            BindingContext = new AddPlantViewModel();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantish.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantView : ContentView
    {
        public PlantView()
        {
            InitializeComponent();
        }
    }
}
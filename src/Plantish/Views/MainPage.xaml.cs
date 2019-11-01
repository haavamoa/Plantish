using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantish.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChangeContent<PlantsView>();
            
        }

        public async void ChangeContent<T>() where T : ContentView
        {
            const uint Speed = 100;
            var animation = Easing.SinIn;
            var contentViewType = typeof(T);
            if (contentViewType == typeof(AddPlantView))
            {
                //AddPlantView.OnStart();
                //await AddPlantView.TranslateTo(AddPlantView.X, 0, Speed, animation);
                await PlantsView.TranslateTo(PlantsView.X, this.Height, Speed, animation);

            }
            else if (contentViewType == typeof(PlantsView))
            {
                PlantsView.OnStart();

                if (this.Height == -1)
                {
                    return;
                }
                //await AddPlantView.TranslateTo(AddPlantView.X, this.Height, Speed, animation);
                await PlantsView.TranslateTo(PlantsView.X, 0, Speed, animation);
            }
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            await Task.Run(
                async () =>
                {

                    if (sender is VisualElement visualElement)
                    {
                        var speed = Resources["speed"];
                        var easing = Resources["easing"];
                        var range = Resources["range"];
                        var scale = Resources["scale"];

                        var animation = Easing.SinIn;
                        switch (easing.ToString().ToLower())
                        {
                            case "sinin":
                                animation = Easing.SinIn;
                                break;
                            case "bouncein":
                                animation = Easing.BounceIn;
                                break;
                            case "bounceout":
                                animation = Easing.BounceOut;
                                break;
                            case "cubicin":
                                animation = Easing.CubicIn;
                                break;
                        }

                        await visualElement.ScaleTo((double)scale, 10000, animation);
                        //visualElement.TranslateTo(visualElement.X, visualElement.TranslationY - (int)range, (uint)speed, animation);
                    }
                }
            );

            
        }
    }
}
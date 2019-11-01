using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Plantish.Views;
using Xamarin.Forms;

namespace Plantish.ViewModels
{
    public class PlantsViewModel
    {
        public PlantsViewModel()
        {
            Plants = new ObservableCollection<PlantViewModel>();
        }

        public void Initialize()
        {
            Plants.Add(
                new PlantViewModel(
                    "Purple Rose",
                    1,
                    DateTime.Now.AddDays(-2), 
                    "https://ae01.alicdn.com/kf/HTB13kt_RVXXXXcSXXXXq6xXFXXXr/5d-diy-diamond-painting-flowers-purple-rose-diamond-painting-bloemen-diamond-embroidery-mosaic-flowers-painting-rhinestones.jpg_640x640.jpg"));
            Plants.Add(
                new PlantViewModel(
                    "Ilex verticillata",
                    14,
                    DateTime.Now.AddDays(-7),
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQhKRG7kiMY9gihS8gSTfuASylMhyDJjr9h9I8mwEIwGIaDIHbUkw"));
            Plants.Add(
                new PlantViewModel(
                    "Sanguinaria",
                    7,
                    DateTime.Now.AddDays(-2),
                    "https://www.studyread.com/wp-content/uploads/2016/04/sanguinaria-300x252.jpg?ezimgfmt=ng:webp/ngcb1"));
            Plants.Add(new PlantViewModel("Lotus", 30, DateTime.Now.AddDays(-10), "https://images-na.ssl-images-amazon.com/images/I/51mUIhGuV1L._SX425_.jpg"));
            Plants.Add(
                new PlantViewModel(
                    "Cactus",
                    365,
                    DateTime.Now.AddDays(-254),
                    "https://cdn.shopify.com/s/files/1/2203/9263/products/Notocactus_magnificus_Balloon_Cactus_1.jpg?v=1532468983"));
            Plants.Add(
                new PlantViewModel(
                    "Apple",
                    30,
                    DateTime.Now.AddDays(-15),
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/2/22/Malus_domestica_a1.jpg/220px-Malus_domestica_a1.jpg"));
            Plants.Add(
                new PlantViewModel(
                    "Apocynum cannabinum",
                    7,
                    DateTime.Now.AddDays(-2),
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Apocynum_cannabinum_6801.JPG/220px-Apocynum_cannabinum_6801.JPG"));
            Plants.Add(
                new PlantViewModel(
                    "Almond",
                    30,
                    DateTime.Now.AddDays(-15),
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Ametllesjuliol.jpg/220px-Ametllesjuliol.jpg"));
            Plants.Add(
                new PlantViewModel(
                    "Fraxinus pennsylvanica",
                    30,
                    DateTime.Now.AddDays(-15),
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Fraxinus_pensylvanica_a1.jpg/220px-Fraxinus_pensylvanica_a1.jpg"));
            Plants.Add(
                new PlantViewModel(
                    "Acer negundo",
                    7,
                    DateTime.Now.AddDays(-2),
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Acnegundo.jpg/220px-Acnegundo.jpg"));
        }

        public ObservableCollection<PlantViewModel> Plants { get; }
    }
}
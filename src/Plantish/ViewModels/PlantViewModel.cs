using System;
using System.Collections.Generic;
using System.Text;

namespace Plantish.ViewModels
{
    public class PlantViewModel
    {
        public PlantViewModel(string name, string whenToWater, string imageUrl)
        {
            Name = name;
            WhenToWater = whenToWater;
            ImageUrl = imageUrl;
        }
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string WhenToWater { get; set; }
    }
}

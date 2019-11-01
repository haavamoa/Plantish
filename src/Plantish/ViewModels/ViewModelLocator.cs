using System;
using System.Collections.Generic;
using System.Text;

namespace Plantish.ViewModels
{
    public class ViewModelLocator
    {
        public void Initialize()
        {
            PlantsViewModel = new PlantsViewModel();
            AddPlantViewModel = new AddPlantViewModel();
        }

        public PlantsViewModel PlantsViewModel { get; private set; }
        public AddPlantViewModel AddPlantViewModel { get; private set; }
    }
}

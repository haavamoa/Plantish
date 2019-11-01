using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace Plantish.ViewModels {
    public class AddPlantViewModel : INotifyPropertyChanged
    {
        private ImageSource m_newPhotoSource;
        private Guid m_newPhotoId;
        private string m_tempPhotoPath;
        private string m_name;
        private PlantViewModel m_newPlant;
        private int m_waterFrequency;
        private DateTime m_lastWateringDate;
        private Action m_navigateBackAction;

        public AddPlantViewModel()
        {
            TakePhotoCommand = new Command(async () => await TakePhoto());
            SavePlantCommand = new Command(Save,() => !string.IsNullOrEmpty(Name) && WaterFrequency != 0);
            DiscardPlantCommand = new Command(Discard);
                m_tempPhotoPath = string.Empty;
            m_newPhotoId = Guid.Empty;
            m_name = string.Empty;
            m_lastWateringDate = DateTime.Now;
        }

        private void Discard()
        {
            m_navigateBackAction.Invoke();
            Reset();
        }

        public void Initialize(Action navigateBackAction)
        {
            m_navigateBackAction = navigateBackAction;
        }

        private async Task TakePhoto()
        {
            await Plugin.Media.CrossMedia.Current.Initialize();
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                {
                    await App.Current.MainPage.DisplayAlert("Camera Permission", "Allow Plantish to access your camera", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                status = results[Permission.Camera];
            }

            if (status == PermissionStatus.Granted)
            {
                if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }


                var newPhotoId  = Guid.NewGuid();
                var file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions() { Directory = "PlantsTemp", Name =  newPhotoId.ToString()});

                if (file == null)
                {
                    return;
                }

                var index = file.Path.LastIndexOf("/");
                if (index > 0)
                {
                    m_tempPhotoPath = file.Path.Substring(0, index);
                }
                m_newPhotoId = newPhotoId;
                NewPhotoSource = ImageSource.FromFile(file.Path);
            }
        }
    
        private async void Save()
        {
            var fileName = m_newPhotoId + ".jpg";
            var sourceFolder = m_tempPhotoPath + "/";
            var destFolder = m_tempPhotoPath.Replace("/PlantsTemp", "") + "/Plants/";
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            File.Move(sourceFolder + fileName, destFolder+fileName);

            //Cleanup temp folder from when we added photos of plant
            Directory.Delete(m_tempPhotoPath, true);

            //Save guid + information into local database

            //Navigate to plants
            m_navigateBackAction.Invoke();
            //Reset all fields 
            Reset();
        }

        private void Reset()
        {
            Name = string.Empty;
            WaterFrequency = 0;
            LastWateringDate = DateTime.Now;
            NewPlant = null;
        }

        public ImageSource NewPhotoSource
        {
            get => m_newPhotoSource;
            private set
            {
                m_newPhotoSource = value;
                OnPropertyChanged();
                UpdateNewPlant();
            }
        }

        private void UpdateNewPlant()
        {
            NewPlant = new PlantViewModel(Name, WaterFrequency, LastWateringDate, NewPhotoSource);
            ((Command)SavePlantCommand).ChangeCanExecute();
        }

        public PlantViewModel NewPlant
        {
            get => m_newPlant;
            set
            {
                m_newPlant = value; 
                OnPropertyChanged(nameof(NewPlant));
            }
        }

        public ICommand TakePhotoCommand { get; }
        public ICommand SavePlantCommand { get; }

        public string Name
        {
            get => m_name;
            set
            {
                m_name = value;
                OnPropertyChanged();
                UpdateNewPlant();
            }
        }

        public int WaterFrequency
        {
            get => m_waterFrequency;
            set
            {
                m_waterFrequency = value;
                OnPropertyChanged();
                UpdateNewPlant();
            }
        }

        public DateTime LastWateringDate
        {
            get => m_lastWateringDate;
            set
            {
                m_lastWateringDate = value;
                OnPropertyChanged();
                UpdateNewPlant();
            }
        }

        public ICommand DiscardPlantCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}   
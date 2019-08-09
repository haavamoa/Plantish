﻿using System;
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
        private ImageSource? m_newPhotoSource;
        private Guid m_newPhotoId;
        private string m_tempPhotoPath;
        private string m_name;

        public AddPlantViewModel()
        {
            TakePhotoCommand = new Command(async () => await TakePhoto());
            SavePlantCommand = new Command(Save,() => !string.IsNullOrEmpty(Name));
                m_tempPhotoPath = string.Empty;
            m_newPhotoId = Guid.Empty;
            m_name = string.Empty;
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

                int index = file.Path.LastIndexOf("/");
                if (index > 0)
                {
                    m_tempPhotoPath = file.Path.Substring(0, index);
                }
                m_newPhotoId = newPhotoId;
                NewPhotoSource = ImageSource.FromFile(file.Path);
            }
        }

        private void Save()
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

        }
        public ImageSource? NewPhotoSource
        {
            get => m_newPhotoSource;
            private set
            {
                m_newPhotoSource = value;
                OnPropertyChanged();
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
                ((Command)SavePlantCommand).ChangeCanExecute();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}   
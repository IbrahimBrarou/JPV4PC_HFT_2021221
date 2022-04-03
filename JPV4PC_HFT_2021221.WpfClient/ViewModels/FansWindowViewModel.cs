using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.WpfClient.Clients;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JPV4PC_HFT_2021221.WpfClient.ViewModels
{
    public class FansWindowViewModel :ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Fans> Fans { get; set; }
        private Fans _selectedFan;

        public Fans SelectedFan
        {
            get => _selectedFan;
            set
            {
                SetProperty(ref _selectedFan, value);
            }
        }
        private int _selectedFanIndex;

        public int SelectedFanIndex
        {
            get => _selectedFanIndex;
            set
            {
                SetProperty(ref _selectedFanIndex, value);
            }
        }
        public RelayCommand AddFanCommand { get; set; }
        public RelayCommand EditFanCommand { get; set; }
        public RelayCommand DeleteFanCommand { get; set; }
        public FansWindowViewModel()
        {
            Fans = new ObservableCollection<Fans>();

            _apiClient
                .GetAsync<List<Fans>>("http://localhost:37793/fans")
                .ContinueWith((fans) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        fans.Result.ForEach((fan) =>
                        {
                            Fans.Add(fan);
                        });
                    });
                });

            AddFanCommand = new RelayCommand(AddFan);
            EditFanCommand = new RelayCommand(EditFan);
            DeleteFanCommand = new RelayCommand(DeleteFan);

        }
        private void AddFan()
        {
            Fans n = new Fans
            {
                Name = SelectedFan.Name,
                City = SelectedFan.City,
                Email = SelectedFan.Email,
                PhoneNumber = SelectedFan.PhoneNumber

            };

            _apiClient
                .PostAsync(n, "http://localhost:37793/fans")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Fans.Add(n);
                    });
                });

        }
        private void EditFan()
        {
            _apiClient
                .PutAsync(SelectedFan, "http://localhost:37793/fans")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedFanIndex;
                        Fans a = SelectedFan;
                        Fans.Remove(SelectedFan);
                        Fans.Insert(i, a);
                    });
                });
        }
        private void DeleteFan()
        {
            _apiClient
                .DeleteAsync(SelectedFan.Id, "http://localhost:37793/fans")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Fans.Remove(SelectedFan);
                    });
                });
        }
    }
}

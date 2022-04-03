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
    class ServicesWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Services> Services { get; set; }
        private Services _selectedService;

        public Services SelectedService
        {
            get => _selectedService;
            set
            {
                SetProperty(ref _selectedService, value);
            }
        }
        private int _selectedServiceIndex;

        public int SelectedServiceIndex
        {
            get => _selectedServiceIndex;
            set
            {
                SetProperty(ref _selectedServiceIndex, value);
            }
        }
        public RelayCommand AddServiceCommand { get; set; }
        public RelayCommand EditServiceCommand { get; set; }
        public RelayCommand DeleteServiceCommand { get; set; }
        public ServicesWindowViewModel()
        {
            Services = new ObservableCollection<Services>();

            _apiClient
                .GetAsync<List<Services>>("http://localhost:37793/services")
                .ContinueWith((services) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        services.Result.ForEach((service) =>
                        {
                            Services.Add(service);
                        });
                    });
                });

            AddServiceCommand = new RelayCommand(AddService);
            EditServiceCommand = new RelayCommand(EditService);
            DeleteServiceCommand = new RelayCommand(DeleteService);

        }
        private void AddService()
        {
            Services n = new Services
            {
                Name=SelectedService.Name,
                Price=SelectedService.Price,
                Rating=SelectedService.Rating
            };

            _apiClient
                .PostAsync(n, "http://localhost:37793/services")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Services.Add(n);
                    });
                });

        }
        private void EditService()
        {
            _apiClient
                .PutAsync(SelectedService, "http://localhost:37793/services")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedServiceIndex;
                        Services a = SelectedService;
                        Services.Remove(SelectedService);
                        Services.Insert(i, a);
                    });
                });
        }
        private void DeleteService()
        {
            _apiClient
                .DeleteAsync(SelectedService.Id, "http://localhost:37793/services")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Services.Remove(SelectedService);
                    });
                });
        }
    }
}

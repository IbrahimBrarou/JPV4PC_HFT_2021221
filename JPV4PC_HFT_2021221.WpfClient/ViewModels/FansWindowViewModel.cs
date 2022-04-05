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
        public IList<KeyValuePair<int, int>> BestFan { get; set; }
        public IList<KeyValuePair<int, int>> WorstFan { get; set; }
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
        public RelayCommand BestFanCommand { get; set; }
        public RelayCommand WorstFanCommand { get; set; }
        public FansWindowViewModel()
        {
            Fans = new ObservableCollection<Fans>();
            BestFan = new List<KeyValuePair<int, int>>();
            WorstFan = new List<KeyValuePair<int, int>>();

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
            _apiClient
               .GetAsync<List<KeyValuePair<int, int>>>("http://localhost:37793/Noncrudfan/BestFans")
               .ContinueWith((Bestfan) =>
               {
                   Application.Current.Dispatcher.Invoke(() =>
                   {
                       Bestfan.Result.ForEach((Bestf) =>
                       {
                           BestFan.Add(Bestf);
                       });
                   });
               });
            _apiClient
              .GetAsync<List<KeyValuePair<int, int>>>("http://localhost:37793/Noncrudfan/WorstFans")
              .ContinueWith((Worstfan) =>
              {
                  Application.Current.Dispatcher.Invoke(() =>
                  {
                      Worstfan.Result.ForEach((Worstf) =>
                      {
                          WorstFan.Add(Worstf);
                      });
                  });
              });

            AddFanCommand = new RelayCommand(AddFan);
            EditFanCommand = new RelayCommand(EditFan);
            DeleteFanCommand = new RelayCommand(DeleteFan);
            BestFanCommand = new RelayCommand(BestArtistMethod);
            WorstFanCommand = new RelayCommand(WorstArtistMethod);

        }

        #region CRUD
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
        #endregion

        #region NON-CRUD
        private void BestArtistMethod()
        {
            new BestFanWindow().Show();
        }
        private void WorstArtistMethod()
        {
            new WorstFanWindow().Show();
        }
        #endregion
    }
}

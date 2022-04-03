using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.WpfClient.ViewModels
{
    public class MainWindowViewModel :ObservableRecipient
    {
        public RelayCommand ManageArtistsCommand { get; set; }
        public RelayCommand ManageFansCommand { get; set; }
        public RelayCommand ManageReservationsCommand { get; set; }
        public RelayCommand ManageServicesCommand { get; set; }



        public MainWindowViewModel()
        {
            ManageArtistsCommand = new RelayCommand(OpenArtistsWindow);
            ManageFansCommand = new RelayCommand(OpenFansWindow);
            ManageReservationsCommand = new RelayCommand(OpenReservationsWindow);
            ManageServicesCommand = new RelayCommand(OpenServicesWindow);
        }
        private void OpenServicesWindow()
        {
            new ServicesWindow().Show();
        }
        private void OpenArtistsWindow()
        {
            new ArtistsWindow().Show();
        }
        private void OpenFansWindow()
        {
            new FansWindow().Show();
        }
        private void OpenReservationsWindow()
        {
            new ReservationsWindow().Show();
        }

    }
}

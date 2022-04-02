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

        public MainWindowViewModel()
        {
            ManageArtistsCommand = new RelayCommand(OpenArtistsWindow);
        }

        private void OpenArtistsWindow()
        {
            new ArtistsWindow().Show();
        }

    }
}

using SpeedTest.Model;
using SpeedTest.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.HelpfullCollections
{
    public class SpeedDataCollectionViewModel
    {
        public ObservableCollection<SpeedDataViewModel> DataColection { get; set; }

        public SpeedTestCommand SingleHistoryDeletedButtonPressed { get; private set; }

        public SpeedDataCollectionViewModel()
        {
            this.DataColection = new ObservableCollection<SpeedDataViewModel>();

            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            DataColection.Add(new SpeedDataViewModel { Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });

            this.SingleHistoryDeletedButtonPressed = new SpeedTestCommand(new Action<object>(SingleHistoryDeleted));
        }

        private async void SingleHistoryDeleted(object param)
        {
            await new Windows.UI.Popups.MessageDialog("SingleHistoryDeleted()").ShowAsync();
        }
    }
}

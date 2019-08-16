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
        public ObservableCollection<SpeedDataViewModel> SpeedDataCollection { get; set; }

        public SpeedDataCollectionViewModel()
        {
            this.SpeedDataCollection = new ObservableCollection<SpeedDataViewModel>();

            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 1, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 2, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 3, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 4, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 5, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 6, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 7, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 8, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 9, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 10, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 11, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 12, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 13, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 14, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 15, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 16, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 17, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 18, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 19, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 20, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 21, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 22, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 23, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 24, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 25, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 26, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 27, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 28, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 29, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 30, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 31, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 32, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 33, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 34, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedDataViewModel { Id = 35, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
        }
    }



}

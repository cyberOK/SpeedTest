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
    public class SpeedDataManager
    {
        public ObservableCollection<SpeedData> SpeedDataCollection { get; set; }

        public SpeedDataManager()
        {
            this.SpeedDataCollection = new ObservableCollection<SpeedData>();

            SpeedDataCollection.Add(new SpeedData { Id = 1, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 2, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 3, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 4, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 5, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 6, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 7, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 8, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 9, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 10, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 11, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 12, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 13, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 14, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 15, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 16, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 17, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 18, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 19, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 20, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 21, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 22, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 23, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 24, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 25, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 26, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 27, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 28, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 29, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 30, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 31, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 32, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 33, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 34, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
            SpeedDataCollection.Add(new SpeedData { Id = 35, Server = "Server", Date = DateTime.Now, Ping = 13, DownloadSpeed = 100, UploadSpeed = 50 });
        }
    }
}

using SpeedTestModel.IPerf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.ServerIPerfProvider
{
    public class ServerIPerfManager
    {
        public int Count()
        {
            using (ServerIPerfContext db = new ServerIPerfContext())
            {
                return db.ServersIPerf.Count();
            }
        }

        public List<ServerIPerf> GetServersList()
        {
            using (ServerIPerfContext db = new ServerIPerfContext())
            {
                return db.ServersIPerf.ToList();
            }
        }

        public async Task SaveSingle(ServerIPerf sample)
        {
            using (ServerIPerfContext db = new ServerIPerfContext())
            {
                await db.ServersIPerf.AddAsync(sample);
                await db.SaveChangesAsync();
            }
        }

        public async Task SaveRange(IList<ServerIPerf> serversCollection)
        {
            using (ServerIPerfContext db = new ServerIPerfContext())
            {
                db.ServersIPerf.AddRange(serversCollection);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAll()
        {
            using (ServerIPerfContext db = new ServerIPerfContext())
            {
                var histories = db.ServersIPerf;

                if (histories != null)
                {
                    db.ServersIPerf?.RemoveRange(histories);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteSingle(int id)
        {
            using (ServerIPerfContext db = new ServerIPerfContext())
            {
                ServerIPerf deletingSample = await db.ServersIPerf.FindAsync(id);

                if (deletingSample != null)
                {
                    db.ServersIPerf?.Remove(deletingSample);
                    await db.SaveChangesAsync();
                }
            }
        }

        public List<ServerIPerf> BaseServerCollection()
        {
            return new List<ServerIPerf>
            {
                new ServerIPerf { IPerf3Server = "iperf.volia.net", Location = "Ukraine", ProviderName = "Volia Kiev", Port = 5201, IsCurrent = false },
                new ServerIPerf { IPerf3Server = "bouygues.iperf.fr", Location = "France", ProviderName = "Telehouse 2", Port = 5205, IsCurrent = false },
                new ServerIPerf { IPerf3Server = "ping.online.net", Location = "France", ProviderName = "Online.net", Port = 5205, IsCurrent = true },
                new ServerIPerf { IPerf3Server = "speedtest.serverius.net", Location = "Netherlands", ProviderName = "Serverius", Port = 5002, IsCurrent = false },
                new ServerIPerf { IPerf3Server = "iperf.eenet.ee", Location = "Estonia", ProviderName = "EENet Tartu", Port = 5201, IsCurrent = false },
                new ServerIPerf { IPerf3Server = "iperf.it-north.net", Location = "Kazakhstan", ProviderName = "Petropavl", Port = 5201, IsCurrent = false },
                new ServerIPerf { IPerf3Server = "iperf.biznetnetworks.com", Location = "Indonesia", ProviderName = "Biznet", Port = 5201, IsCurrent = false },
                new ServerIPerf { IPerf3Server = "iperf.scottlinux.com", Location = "USA, California", ProviderName = "Hurricane Fremont 2", Port = 5201, IsCurrent = false },
                new ServerIPerf { IPerf3Server = "iperf.he.net", Location = "USA, California", ProviderName = "Hurricane Fremont 1", Port = 5201, IsCurrent = false }
            };
        }
    }
}

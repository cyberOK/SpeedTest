using SpeedTestModel.Iperf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.HistoryManager
{
    public class HistoryManager
    {
        public int Count()
        {
            using (HistoryContext db = new HistoryContext())
            {
                return db.History.Count();
            }
        }

        public IList<SpeedData> GetHistoryList()
        {
            using (HistoryContext db = new HistoryContext())
            {
                return db.History.ToList();
            }
        }

        public async Task Save(SpeedData sample)
        {
            using (HistoryContext db = new HistoryContext())
            {
                await db.History.AddAsync(sample);
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete()
        {
            using (HistoryContext db = new HistoryContext())
            {
                var histories = db.History;

                if (histories != null)
                {
                    db.History?.RemoveRange(histories);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteSingleSample(int id)
        {
            using (HistoryContext db = new HistoryContext())
            {
                SpeedData deletingSample = await db.History.FindAsync(id);

                if (deletingSample != null)
                {
                    db.History?.Remove(deletingSample);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}

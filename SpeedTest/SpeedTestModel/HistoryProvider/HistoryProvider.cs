using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.HistoryProvider
{
    public class HistoryProvider
    {
        public int Count()
        {
            using (SpeedDataContext db = new SpeedDataContext())
            {
                return db.SpeedDatas.Count();
            }
        }

        public IList<SpeedData> GetHistoryList()
        {
            using (SpeedDataContext db = new SpeedDataContext())
            {
                return db.SpeedDatas.ToList();
            }
        }

        public async Task Save(SpeedData sample)
        {
            using (SpeedDataContext db = new SpeedDataContext())
            {
                await db.SpeedDatas.AddAsync(sample);
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete()
        {
            using (SpeedDataContext db = new SpeedDataContext())
            {
                var histories = db.SpeedDatas;

                if (histories != null)
                {
                    db.SpeedDatas?.RemoveRange(histories);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteSingleSample(int id)
        {
            using (SpeedDataContext db = new SpeedDataContext())
            {
                SpeedData deletingSample = await db.SpeedDatas.FindAsync(id);

                if (deletingSample != null)
                {
                    db.SpeedDatas?.Remove(deletingSample);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}

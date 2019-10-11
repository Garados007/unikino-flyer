using System;
using System.Collections.Generic;
using System.Text;
using MaxLib.DB;
using System.IO;
using UnikinoFlyer.Data.Properties;

namespace UnikinoFlyer.Data
{
    public class Factory : IDisposable
    {
        private Database db;
        private DbFactory fact;

        public Factory(string file, bool required)
        {
            if (required && !File.Exists(file))
            {
                throw new FileNotFoundException("db file not found", file);
            }
            db = new Database(file, 
                Resources.ResourceManager.GetString("CheckDb"),
                Resources.ResourceManager.GetString("CreateDb"));
            fact = new DbFactory(db);
        }

        public IEnumerable<DelayedSwitch> AllDelayedSwitches()
        {
            return fact.LoadAll<DelayedSwitch>();
        }

        public IEnumerable<DelayedSwitch> DueToUploadDelayedSwitches(DateTime now)
        {
            return fact.LoadMatch<DelayedSwitch>(
                new DbValue("Upload", now, DbComp.lteq),
                new DbValue("Due", now, DbComp.gteq),
                new DbValue("UploadedAt", null)
            );
        }

        public void SetDelayedSwitch(params DelayedSwitch[] switches)
        {
            foreach (var s in switches)
                if (s.Id == 0)
                    fact.Add(s);
                else fact.Update(s);
        }

        public void Dispose()
        {
            fact.Dispose();
            db.Dispose();
        }
    }
}

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhc_noaa.Controllers
{
    public class MigrateController : BaseController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<IEnumerable<string>> Get(int count = 5)
        {
            try
            {
                var files = DirInfo.GetOldestFiles(count);
                var fileNames = files.Select(x => x.Name).ToArray();
                foreach (var file in files)
                    await file.Upload(ConnectionString);
                return fileNames;
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }
        }
    }
}

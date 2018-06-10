using System.Collections.Generic;
using System.Linq;

namespace nhc_noaa.Controllers
{
    public class MigrateController : BaseController
    {
        public IEnumerable<string> Get()
        {
            var files = DirInfo.GetOldestFiles(5).Select(x => x.FullName);
            //TODO: move files to the Azure Storage
            return files;
        }
    }
}

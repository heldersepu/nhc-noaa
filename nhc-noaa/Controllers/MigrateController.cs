using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhc_noaa.Controllers
{
    public class MigrateController : BaseController
    {
        public async Task<IEnumerable<string>> Get()
        {
            var files = DirInfo.GetOldestFiles(5);
            var fileNames = files.Select(x => x.Name).ToArray();
            foreach (var file in files)
            {
                await file.Upload(ConnectionString);
            }
            return fileNames;
        }
    }
}

using System;
using System.IO;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

namespace nhc_noaa.Controllers
{
    public class ImagesController : BaseController
    {
        [HttpGet]
        public IEnumerable<string> EastAtlantic(int count = 20, DateTime? min = null, DateTime? max = null)
        {
            return DirInfo.GetLatestFiles(count)
                .Where(x => (
                        (x.CreationTime > (min ?? DateTime.MinValue)) &&
                        (x.CreationTime < (max ?? DateTime.MaxValue)))
                    )
                .Select(x => x.Name);
        }

        private DirectoryInfo DirInfo
        {
            get
            {
                return new DirectoryInfo(baseDir(east_atl_path));
            }
        }
    }
}

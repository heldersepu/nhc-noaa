using System;
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
            return DirInfo.GetLatestFiles(count, min, max).Select(x => x.Name);
        }
    }
}

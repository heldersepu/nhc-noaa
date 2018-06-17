using nhc_noaa.Models;
using System.Linq;

namespace nhc_noaa.Controllers
{
    public class StatsController : BaseController
    {
        public ImageStats Get()
        {
            var images = new ImageStats();
            images.Local = DirInfo.GetFiles().Length;
            images.Cloud = CloudDir.ListFilesAndDirectories().Count();
            return images;
        }
    }
}

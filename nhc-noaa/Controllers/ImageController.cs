namespace nhc_noaa.Controllers
{
    public class ImageController : BaseController
    {
        public string Get(string name)
        {
            return CloudDir.GetFileReference(name).Uri.AbsolutePath;
        }        
    }
}
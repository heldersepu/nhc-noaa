using Swagger.Net.Annotations;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace nhc_noaa.Controllers
{
    public class ImageController : BaseController
    {
        [SwaggerResponse(200, mediaType: "image/png")]
        public async Task<HttpResponseMessage> Get(string name)
        {
            var response = new HttpResponseMessage();
            var memStream = new MemoryStream();
            await CloudDir.GetFileReference(name).DownloadRangeToStreamAsync(memStream, null, null);
            memStream.Position = 0;
            response.Content = new StreamContent(memStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return response;
        }
    }
}
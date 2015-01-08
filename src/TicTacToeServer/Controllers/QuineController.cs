using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace TicTacToeServer.Controllers
{
    [RoutePrefix("quine")]
    public class QuineController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var file = ConfigurationManager.AppSettings["PathToDownload"];
            var stream = new FileStream(file, FileMode.Open);
 
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(file)
            };

            return result;
        }
    }
}
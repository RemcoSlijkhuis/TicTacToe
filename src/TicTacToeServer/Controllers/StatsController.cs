using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TicTacToeServer.Controllers
{
    /// <summary>
    /// Serves as a global stats controller
    /// </summary>
    [RoutePrefix("stats")]
    public class StatsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(GameStateHelper.GetStatsHtml(), Encoding.UTF8, "text/html")
            };
        }
    }
}
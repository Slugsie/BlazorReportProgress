using BlazorReportProgress.Server.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

namespace BlazorReportProgress.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlowProcessController : ControllerBase
    {
        private readonly ILogger<SlowProcessController> _logger;
        private readonly IHubContext<ProgressHub> _hubController;

        public SlowProcessController(
            ILogger<SlowProcessController> logger,
            IHubContext<ProgressHub> hubContext)
        {
            _logger = logger;
            _hubController = hubContext;
        }

        [HttpGet]
        public IEnumerable<int> Get()
        {
            List<int> retVal = new();

            for (int loop = 0; loop < 10; loop++)
            {
                _hubController.Clients.All.SendAsync("ProgressReport", loop.ToString());

                retVal.Add(loop);
                Thread.Sleep(500);
            }

            return retVal;
        }
    }
}
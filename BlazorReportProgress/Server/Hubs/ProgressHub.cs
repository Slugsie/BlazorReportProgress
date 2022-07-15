using Microsoft.AspNetCore.SignalR;

namespace BlazorReportProgress.Server.Hubs
{
    public class ProgressHub : Hub
    {
        public async Task ReportProgress(string message)
        {
            await Clients.Caller.SendAsync("ProgressReport", message);
        }
    }
}

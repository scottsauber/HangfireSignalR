using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.SignalR;

namespace HangfireSignalR.Web.Jobs
{
    public class JobHub : Hub
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IThingDoerService _thingDoerService;

        public JobHub(IBackgroundJobClient backgroundJobClient, IThingDoerService thingDoerService)
        {
            _backgroundJobClient = backgroundJobClient;
            _thingDoerService = thingDoerService;
        }
        
        public async Task StartJob(string storeCode)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, storeCode);
            _backgroundJobClient.Enqueue(() => _thingDoerService.DoTheThings(storeCode));
        }
    }
}
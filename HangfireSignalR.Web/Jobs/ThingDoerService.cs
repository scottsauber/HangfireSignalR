using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HangfireSignalR.Web.Jobs
{
    public interface IThingDoerService
    {
        Task DoTheThings(string storeCode);
    }

    public class ThingDoerService : IThingDoerService
    {
        private readonly IHubContext<JobHub> _hubContext;

        public ThingDoerService(IHubContext<JobHub> hubContext)
        {
            _hubContext = hubContext;
        }
        
        public async Task DoTheThings(string storeCode)
        {
            await _hubContext.Clients.Groups(storeCode).SendAsync("reportProgress", "Starting to do the things");
            await Task.Delay(5000);
            await _hubContext.Clients.Groups(storeCode).SendAsync("reportProgress", "Finished doing the things");
        } 
    }
}
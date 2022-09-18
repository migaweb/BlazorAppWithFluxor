using Microsoft.AspNetCore.SignalR;

namespace BlazorAppWithFluxor.Server.Hubs
{
  public class CounterHub : Hub
  {
    public async Task SendCount(int count)
    {
      await Clients.Others.SendAsync("ReceiveCount", count);
    }
  }
}

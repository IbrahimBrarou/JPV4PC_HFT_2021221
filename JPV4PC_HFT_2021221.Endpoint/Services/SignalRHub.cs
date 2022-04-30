using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Endpoint.services
{
    public class SignalRHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Caller.SendAsync("Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

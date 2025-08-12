using ETicaret.Application.Abstractions.Hubs;
using ETicaret.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETicaret.SignalR.HubServices;

public class CustomerHubService(IHubContext<CustomerHub> hubContext) : ICustomerHubService
{
    
}
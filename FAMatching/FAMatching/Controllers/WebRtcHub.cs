using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class WebRtcHub : Hub
{
    public async Task SendOffer(string receiverId, string offer)
    {
        await Clients.Client(receiverId).SendAsync("ReceiveOffer", offer);
    }

    public async Task SendAnswer(string receiverId, string answer)
    {
        await Clients.Client(receiverId).SendAsync("ReceiveAnswer", answer);
    }

    public async Task SendIceCandidate(string receiverId, string candidate)
    {
        await Clients.Client(receiverId).SendAsync("ReceiveIceCandidate", candidate);
    }
}

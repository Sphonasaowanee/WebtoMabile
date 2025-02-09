using Microsoft.AspNetCore.Mvc; 
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace FAMatching.Controllers
{
    [ApiController]
    [Route("signal")]
    public class WebRtcSignalingController : ControllerBase
    {
        private static ConcurrentDictionary<string, WebSocket> _clients = new();

        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                string clientId = Guid.NewGuid().ToString();
                _clients[clientId] = webSocket;

                await HandleWebSocket(clientId, webSocket);
            }
        }

        private async Task HandleWebSocket(string clientId, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result;

            while (webSocket.State == WebSocketState.Open)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);

                // ส่งต่อข้อความไปยังลูกค้ารายอื่น (Signaling)
                foreach (var client in _clients)
                {
                    if (client.Key != clientId)
                    {
                        await client.Value.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count),
                            result.MessageType, result.EndOfMessage, CancellationToken.None);
                    }
                }
            }

            _clients.TryRemove(clientId, out _);
        }
    }
}

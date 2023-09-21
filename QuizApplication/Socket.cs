using System.Net;
using System.Net.WebSockets;
using System.Text;
using QuizApplication.Data;

namespace QuizApplication;

public class Socket
{
    private static WebSocket? _socket;

    public static async Task New(HttpContext context)
    {
        if (_socket is not null)
        {
            return;
        }
        
        if (!context.WebSockets.IsWebSocketRequest)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }
        
        _socket = await context.WebSockets.AcceptWebSocketAsync();

        await SendMessage("Hello Galaxy!");
    }
    
    public static Task Send<T>(T Value)
    {
        if (_socket is null)
        {
            return Task.CompletedTask;
        }
        
        var json = Value.ToJson();
        return SendMessage(json);
    }

    public static Task SendMessage(string message)
    {
        if (_socket is null)
        {
            return Task.CompletedTask;
        }
        
        var bytes = Encoding.UTF8.GetBytes(message);
        var buffer = new ArraySegment<byte>(bytes);
        return _socket.SendAsync(
            buffer, 
            WebSocketMessageType.Text, 
            true, 
            CancellationToken.None);
    }
}
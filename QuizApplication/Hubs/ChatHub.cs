using Microsoft.AspNetCore.SignalR;

namespace QuizApplication.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(string user, string message)
    {
        Console.WriteLine($"{user}: {message}");
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public Task SendValue(string value)
    {
        Console.WriteLine($"{value}");
        return Clients.All.SendAsync("ReceiveMessage", value);
    }
}
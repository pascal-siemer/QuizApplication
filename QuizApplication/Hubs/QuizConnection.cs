using Microsoft.AspNetCore.SignalR.Client;

namespace QuizApplication.Hubs;

public class QuizConnection
{
    public QuizConnection()
    {
        var connection = new HubConnectionBuilder();
    }
}
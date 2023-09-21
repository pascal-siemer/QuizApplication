using Microsoft.AspNetCore.SignalR;
using QuizApplication.Data;

namespace QuizApplication.Hubs;

public class QuizHub : Hub
{
    private HttpClient _client;

    public QuizHub()
    {
        _client = new HttpClient();
    }
    
    public Task SendMessage(string message)
    {
        Console.WriteLine(message);
        return Clients.All.SendAsync("ReceiveMessage", "Hello Galaxy");
    }

    public async Task GetQuestion(int identifier)
    {
        Console.WriteLine($"Question #{identifier} requested");
        //var question = new Question(1, "Hello?", "World", new() { "World", "Mom", "Galaxy", "Universe" });
        var question = await GetQuestionFromApi(identifier);
        await Clients.All.SendAsync("ReceiveQuestion", question);
    }

    private Task<Question?> GetQuestionFromApi(int identifier)
    {
        return _client.GetFromJsonAsync<Question?>($"http://localhost:5234/question/{identifier}");
    }
    
}
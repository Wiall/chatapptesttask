using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace chatappbackend;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        // Якщо інтегруєш Azure Cognitive Services — сюди вставиш аналіз
        var sentiment = AnalyzeSentiment(message); // Псевдофункція — її потрібно реалізувати
        await Clients.All.SendAsync("ReceiveMessage", user, message, sentiment);
    }

    // Наприклад, заглушка для демонстрації (тимчасово)
    private string AnalyzeSentiment(string message)
    {
        // Проста логіка для тесту
        if (message.Contains("good") || message.Contains("great"))
            return "positive";
        if (message.Contains("bad") || message.Contains("terrible"))
            return "negative";
        return "neutral";
    }
}

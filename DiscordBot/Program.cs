using System.Net;
using Discord;
using Discord.WebSocket;

public class Program
{
    private DiscordSocketClient client;

    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        client = new DiscordSocketClient();
        client.Log += Log;

        var token = await File.ReadAllTextAsync(Path.Combine(typeof(Program).Assembly.Location, "..", "token.txt"));
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
        client.MessageReceived += OnMessage;

        await Task.Delay(-1);
    }

    private async Task OnMessage(SocketMessage arg)
    {
        if (arg.Content.ToLower().StartsWith("scplr_"))
        {
            await arg.Channel.SendMessageAsync($"(SCP: Labrat) {await LabratAPI.Handle(arg.Content.Remove(0, 6))}");
        }
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
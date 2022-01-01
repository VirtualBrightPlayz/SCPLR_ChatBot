public class Program
{
    public static Task Main(string[] args) => MainAsync();
    
    public static async Task MainAsync()
    {
        await LabratAPI.Text("https://google.com\nh");
    }
}
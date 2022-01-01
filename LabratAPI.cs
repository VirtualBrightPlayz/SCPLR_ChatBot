public static class LabratAPI
{
    public const string allowedChars = "abcdefghijklmnopqrstuvwxyz0123456789_";
    public static async Task<string> Spawn(string id)
    {
        HttpClient http = new HttpClient();
        var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"http://127.0.0.1:8090/spawn?{id}="));
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> Die()
    {
        HttpClient http = new HttpClient();
        var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"http://127.0.0.1:8090/die"));
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> Text(string text, float time = 10f)
    {
        string str = string.Empty;
        for (int i = 0; i < text.Length; i++)
        {
            str += Uri.HexEscape(text[i]);
        }
        HttpClient http = new HttpClient();
        var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"http://127.0.0.1:8090/text?text={str}&time={time}"));
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> Handle(string arg, string sender = "")
    {
        var spl = arg.Split(' ');
        switch (spl[0].ToLower())
        {
            case "spawn":
            {
                if (spl.Length == 2)
                {
                    string i = string.Empty;
                    foreach (var item in spl[1].ToLower())
                    {
                        if (allowedChars.Contains(item))
                            i += item;
                    }
                    string ret = await Spawn(i);
                    if (!string.IsNullOrEmpty(sender))
                        await Text($"{sender} spawned in {i} for you.");
                    return ret;
                }
            }
            break;
            case "die":
            {
                    string ret = await Die();
                    if (!string.IsNullOrEmpty(sender))
                        await Text($"{sender} killed you.");
                    return ret;
            }
        }
        return string.Empty;
    }
}
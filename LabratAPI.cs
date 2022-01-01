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

    public static async Task<string> Handle(string arg)
    {
        var spl = arg.Split(' ');
        switch (spl[0].ToLower())
        {
            case "spawn":
            {
                if (spl.Length == 2)
                {
                    HttpClient http = new HttpClient();
                    string i = string.Empty;
                    foreach (var item in spl[1].ToLower())
                    {
                        if (allowedChars.Contains(item))
                            i += item;
                    }
                    return await Spawn(i);
                }
            }
            break;
            case "die":
            {
                return await Die();
            }
            case "text":
            {
                string str = string.Empty;
                for (int i = 1; i < spl.Length; i++)
                {
                    str += spl[i] + " ";
                }
                str = str.Trim();
                return await Text(str);
            }
        }
        return string.Empty;
    }
}
namespace MyMovieList;

public static class Global
{
    public static string? TMDBToken { get; private set; }

    public static void GetToken()
    {
        var envFile = Environment.CurrentDirectory + @"\.env";
        if (!File.Exists(envFile))
            throw new Exception("can't found .env file");
        StreamReader sr = new StreamReader(envFile);
        var line = sr.ReadLine();
        if (string.IsNullOrEmpty(line))
            throw new Exception("can't found token");
        TMDBToken = line.ToString();
    }
}
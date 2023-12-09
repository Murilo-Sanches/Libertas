namespace Libertas.Source.Core.Services;

class Logger
{
    private static readonly Dictionary<string, string> _emojis = new()
    {
        { "success", "MS - ✅ " },
        { "error", "MS - ❌ " },
        { "info", "MS - ℹ️ " },
        { "warning", "MS - ⚠️ " }
    };

    public static void Success(string str)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{Environment.NewLine}{_emojis["success"]}{str}{Environment.NewLine}");
        Console.ResetColor();
    }

    public static void Error(string str)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{Environment.NewLine}{_emojis["error"]}{str}{Environment.NewLine}");
        Console.ResetColor();
    }

    public static void Info(string str)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"{Environment.NewLine}{_emojis["info"]}{str}{Environment.NewLine}");
        Console.ResetColor();
    }

    public static void Warning(string str)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{Environment.NewLine}{_emojis["warning"]}{str}{Environment.NewLine}");
        Console.ResetColor();
    }
}

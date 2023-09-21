using System.Text.Json;

namespace QuizApplication;

public static class GenericExtensions
{
    public static string ToJson<T>(this T instance)
    {
        return JsonSerializer.Serialize(instance);
    }
}
using System.Text.Json;

namespace Extensions.Session;

public static class SessionExtensions
{
    public static T GetObjectFromJson<T>(this ISession session, string Key)
    {
        var value = session.GetString(Key);
        if (value == null)
            return default;

        return JsonSerializer.Deserialize<T>(value);
    }

    public static void SetObjectASJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }
}

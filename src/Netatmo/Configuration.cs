using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl.Http.Configuration;
using Netatmo.Converters;

namespace Netatmo;

public static class Configuration
{
    public static JsonSerializerOptions JsonSerializerOptions =>
        new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, Converters = { new TimestampToInstantConverter(), new StringToDateTimeZoneConverter() } };

    public static void ConfigureRequest(FlurlHttpSettings settings)
    {
        // something like maybe miss : jsonSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions);
    }
}
using System.Text.Json.Serialization;

namespace ManageBotPlus
{
    public record TokenModel([property: JsonPropertyName("token")] string Token);
    public record AllowedGuildsModel([property: JsonPropertyName("allowedGuilds")] List<ulong> AllowedGuilds);
}


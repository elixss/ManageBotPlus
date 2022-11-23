using System.Text.Json.Serialization;

namespace ManageBotPlus
{
    public record TokenModel([property: JsonPropertyName("token")] string Token, [property: JsonPropertyName("testToken")] string TestToken);
    public record AllowedGuildsModel([property: JsonPropertyName("allowedGuilds")] List<ulong> AllowedGuilds);
}


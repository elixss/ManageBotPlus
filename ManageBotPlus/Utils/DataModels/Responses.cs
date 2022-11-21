using System.Text.Json.Serialization;

namespace ManageBotPlus
{
    public record NsfwResponse([property: JsonPropertyName("status")] string Status, [property: JsonPropertyName("message")] string Message);
}
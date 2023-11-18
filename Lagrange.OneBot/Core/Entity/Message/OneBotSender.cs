using System.Text.Json.Serialization;

namespace Lagrange.OneBot.Core.Entity.Message;

[Serializable]
public class OneBotSender(uint userId, string nickname)
{
    [JsonPropertyName("user_id")] public uint UserId { get; set; } = userId;

    [JsonPropertyName("nickname")] public string Nickname { get; set; } = nickname;

    [JsonPropertyName("sex")] public string Sex { get; set; } = "unknown";

    [JsonPropertyName("age")] public uint Age { get; set; } = 0;
}
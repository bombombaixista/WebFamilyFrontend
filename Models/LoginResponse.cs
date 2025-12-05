using System.Text.Json.Serialization;

namespace WebFamilyFrontEnd.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}

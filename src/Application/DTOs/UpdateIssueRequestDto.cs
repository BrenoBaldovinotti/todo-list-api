using System.Text.Json.Serialization;

namespace Application.DTOs;

public class UpdateIssueRequestDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

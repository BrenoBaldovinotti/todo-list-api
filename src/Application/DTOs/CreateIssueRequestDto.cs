using System.Text.Json.Serialization;

namespace Application.DTOs;

public class CreateIssueRequestDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

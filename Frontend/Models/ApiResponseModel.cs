using System.Text.Json.Serialization;

namespace _Frontend.Models;

public class ApiResponseModel<T>
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    public object? Errors { get; set; }

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
}

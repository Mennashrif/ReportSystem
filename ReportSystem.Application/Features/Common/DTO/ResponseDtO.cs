using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace ReportSystem.Application.Features.Common.DTO
{
    public class ResponseDTO
    {
        public int StatusCode { get; set; } = 200;
        public dynamic? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get => ErrorMessage.IsNullOrEmpty(); }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}

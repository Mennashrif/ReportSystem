namespace ReportSystem.Application.Features.Account.Queries.Login
{
    public record LoginResponse (string Token, string? Role)
    {
    }
}

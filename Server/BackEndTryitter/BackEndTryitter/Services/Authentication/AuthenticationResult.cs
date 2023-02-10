namespace BackEndTryitter.Services.Authentication;

public record AuthenticationResult(
    Guid Id,
    string FullName,
    string Email,
    string UserName,
    int CurrentModule,
    string Token);
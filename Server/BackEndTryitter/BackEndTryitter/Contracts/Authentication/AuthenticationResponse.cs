namespace BackEndTryitter.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string FullName,
    string Username,
    string Email,
    int CurrentModule,
    string Token);
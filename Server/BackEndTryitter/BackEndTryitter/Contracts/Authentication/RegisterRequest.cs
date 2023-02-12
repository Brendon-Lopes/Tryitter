namespace BackEndTryitter.Contracts.Authentication;

public record RegisterRequest(
    string FullName,
    string Username,
    string Email,
    string Password,
    int CurrentModule);